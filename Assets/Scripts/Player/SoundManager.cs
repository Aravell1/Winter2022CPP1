using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    static SoundManager _instance = null;

    public static SoundManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    List<AudioSource> currentAudioSources = new List<AudioSource>();
    bool didPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        currentAudioSources.Add(gameObject.GetComponent<AudioSource>());
    }

    private void Update()
    {
        if (GameManager.instance.playerInstance)
        {
            Vector3 soundTransform;

            soundTransform = transform.position;

            soundTransform.x = GameManager.instance.playerInstance.transform.position.x + 5;

            transform.position = soundTransform;

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

    public void Play(AudioClip clip, AudioMixerGroup group)
    {
        foreach(AudioSource source in currentAudioSources)
        {
            if (source.isPlaying)
            {
                continue;
            }
            didPlay = true;
            source.PlayOneShot(clip);
            source.outputAudioMixerGroup = group;
            break;
        }

        if (!didPlay)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            currentAudioSources.Add(temp);
            temp.PlayOneShot(clip);
            temp.outputAudioMixerGroup = group;
        }

        didPlay = false;
    }
}
