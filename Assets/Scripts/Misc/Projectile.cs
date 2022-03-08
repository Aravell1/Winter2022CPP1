using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damageValue;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
        {
            lifetime = 2.0f;
        }

        if (damageValue <= 0)
        {
            damageValue = 2;
        }

        if (gameObject.tag == "PlayerProjectile")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "PlayerProjectile")
        {
            //Projectile will not destroy itself on the floor, but will on the wall
            if (collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Player")
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            else if (collision.gameObject.tag == "Enemy")
            {            
                Enemy e = collision.gameObject.GetComponent<Enemy>();

                if (e)
                {
                    e.TakeDamage(damageValue);
                }
                Destroy(gameObject);
            }
            else if (collision.gameObject.name != "BoundFloor")
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.tag == "EnemyProjectile")
        {
            //Projectile will not destroy itself on the floor, but will on the wall
            if (collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Enemy")
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            else if (collision.gameObject.tag == "Player")
            {
                GameManager.instance.lives--;
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
