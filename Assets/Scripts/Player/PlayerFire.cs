using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class PlayerFire : MonoBehaviour
{
    public bool verbose = false;

    SpriteRenderer sr;
    Animator anim;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public float projectileSpeed;
    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (projectileSpeed <= 0)
        {
            projectileSpeed = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            if (verbose)
            {
                Debug.Log("Inspector Values Not Set");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack 0");
        }
    }

    public void FireProjectile()
    {
        if (sr.flipX)
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            temp.speed = -projectileSpeed;
        }
        else
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            temp.speed = projectileSpeed;
        }
    }
}
