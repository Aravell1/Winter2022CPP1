using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Pickups : MonoBehaviour
{
    enum CollectibleType
    {
        POWERUP,
        SCORE,
        LIFE
    }

    [SerializeField] CollectibleType curCollectible;
    [SerializeField] AudioClip pickupSound;
    AudioSource myAudioSource;
    public int ScoreValue;
    public AudioMixerGroup soundFXGroup;

    private void Start()
    {
        if (curCollectible == CollectibleType.LIFE)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-2, rb.velocity.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!myAudioSource)
        {
            myAudioSource = GetComponent<AudioSource>();
        }
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.instance.Play(pickupSound, soundFXGroup);
            switch (curCollectible)
            {
                case CollectibleType.POWERUP:
                    collision.gameObject.GetComponent<Player>().StartJumpForceChange();
                    GameManager.instance.score += ScoreValue;
                    break;
                case CollectibleType.SCORE:
                    GameManager.instance.score += ScoreValue;
                    break;
            }
            Destroy(gameObject);
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!myAudioSource)
        {
            myAudioSource = GetComponent<AudioSource>();
        }
        if (collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (collision.gameObject.tag == "Player")
        {
            SoundManager.instance.Play(pickupSound, soundFXGroup);
            //Player curPlayerScript = collision.gameObject.GetComponent<Player>();
            switch (curCollectible)
            {
                case CollectibleType.LIFE:
                    GameManager.instance.score += ScoreValue;
                    GameManager.instance.lives++;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
