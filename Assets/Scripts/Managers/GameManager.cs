using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    
    int _score = 0;
    int _lives = 1;
    public int maxLives = 3;
    public GameObject playerPrefab;

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
                //respawn code goes here
                Destroy(playerInstance);
                SpawnPlayer(currentLevel.spawnPoint);
            }

            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }


            if (_lives < 0)
            {
                SceneManager.LoadScene("GameOver");
            }

            

            Debug.Log("Lives Set to: " + lives.ToString());
        }
    }

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
        }
    }



    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Title")
            {
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                SceneManager.LoadScene("Title");
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
    }

    public void SpawnPlayer(Transform spawnlocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnlocation.position, spawnlocation.rotation);
    }
}
