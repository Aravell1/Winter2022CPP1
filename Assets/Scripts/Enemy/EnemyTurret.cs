using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [SerializeField] float projectileForce;
    [SerializeField] float projectileFireRate;
    [SerializeField] float turretFireDistance;

    float timeOfLastFire;

    public Transform projectileSpawnPointRight;
    public Transform projectileSpawnPointLeft;

    public Projectile projectilePrefab;




    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (projectileForce <= 0)
        {
            projectileForce = 7f;
        }

        if (projectileFireRate <= 0)
        {
            projectileFireRate = 2.0f;
        }

        if (turretFireDistance <= 0)
        {
            turretFireDistance = 7f;
        }

        if (!projectilePrefab)
        {
            if (verbose)
            {
                Debug.Log("Prokectile Prefab has not been set on " + name);
            }
        }
        if (!projectileSpawnPointLeft)
        {
            if (verbose)
            {
                Debug.Log("Prokectile Spawn Point Left has not been set on " + name);
            }
        }
        if (!projectileSpawnPointRight)
        {
            if (verbose)
            {
                Debug.Log("Prokectile Spawn Point Right has not been set on " + name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float distance = Vector2.Distance(transform.position, GameManager.instance.playerInstance.transform.position);

        if (!anim.GetBool("Fire"))
        {
            if (distance <= turretFireDistance)
            {
                if (transform.position.x > GameManager.instance.playerInstance.transform.position.x)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }

                if (Time.time > timeOfLastFire + projectileFireRate)
                {
                    if (curPlayingClip[0].clip.name != "Fire")
                    {
                        anim.SetBool("Fire", true);
                    }
                }
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

    public void Fire()
    {
        timeOfLastFire = Time.time;

        Vector2 dirToPlayer = (GameManager.instance.playerInstance.transform.position - transform.position).normalized;
        Projectile temp;
                
        if (sr.flipX)
        {
            temp = Instantiate(projectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointLeft.rotation);
        }
        else
        {
            temp = Instantiate(projectilePrefab, projectileSpawnPointRight.position, projectileSpawnPointRight.rotation);
        }
        temp.gameObject.GetComponent<Rigidbody2D>().velocity = dirToPlayer * projectileForce;
    }

    public override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }

    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Debug.Log("Enemy MagiKoopa took " + damage + " damage");
        Debug.Log("Enemy MagiKoopa health " + health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

}
