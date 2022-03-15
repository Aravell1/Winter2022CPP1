using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public bool gamePaused = false;
    
    int _score = 0;
    int _lives = 1;
    public int maxLives = 3;
    public GameObject playerPrefab;

    public AudioClip playerDamage;
    public AudioClip gameOver;
    public AudioMixerGroup soundFXGroup;

    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            if (_lives > value)
            {
                if (lives > 0)
                {
                    SoundManager.instance.Play(playerDamage, soundFXGroup);
                }
                Destroy(playerInstance);
                SpawnPlayer(currentLevel.spawnPoint);
            }

            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            onLifeValueChange.Invoke(value);

            if (_lives < 0)
            {
                SceneManager.LoadScene("GameOver");
                SoundManager.instance.Play(gameOver, soundFXGroup);
            }           

            Debug.Log("Lives Set to: " + lives.ToString());
        }
    }

    [HideInInspector] public UnityEvent<int> onLifeValueChange;
    [HideInInspector] public UnityEvent<int> onScoreValueChange;
    [HideInInspector] public GameObject playerInstance;
    [HideInInspector] public Level currentLevel;

    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            Debug.Log("Score Set to: " + score.ToString());
            onScoreValueChange.Invoke(value);
        }
    }

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    public void SpawnPlayer(Transform spawnlocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnlocation.position, spawnlocation.rotation);
    }
}
