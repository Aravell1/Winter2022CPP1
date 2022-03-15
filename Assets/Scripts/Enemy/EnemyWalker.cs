using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class EnemyWalker : Enemy
{
    Rigidbody2D rb;

    [SerializeField] float speed;

    public AudioClip enemyDeath;
    public AudioMixerGroup soundFXGroup;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        
        if (speed <= 0)
        {
            speed = 5.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Death") & !anim.GetBool("Squished"))
        {
            if (sr.flipX)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }

        if (GameObject.Find("GameManager").GetComponent<GameManager>().gamePaused == true)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }

    public void IsSquished()
    {
        anim.SetBool("Squished", true);
        rb.velocity = Vector2.zero;
        Destroy(transform.parent.gameObject, 1.0f);
    }

    public override void Death()
    {
        SoundManager.instance.Play(enemyDeath, soundFXGroup);
        base.Death();
        anim.SetBool("Death", true);
        rb.velocity = Vector2.zero;
        Destroy(transform.parent.gameObject, 1.0f);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Debug.Log("Enemy Koopa took " + damage + " damage");
        Debug.Log("Enemy Koopa health " + health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
