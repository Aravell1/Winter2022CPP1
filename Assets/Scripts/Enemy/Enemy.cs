using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : MonoBehaviour
{

    public bool verbose;

    [SerializeField] protected int maxHealth;

    protected int _health;

    protected SpriteRenderer sr;
    protected Animator anim;

    public int health
    {
        get { return _health; }
        set 
        {
            _health = value;
            if (_health > maxHealth)
            {
                _health = maxHealth;
            }
            if (_health <= 0)
            {
                Death();
            }
        }
    }

    public virtual void Death()
    {
        if (verbose)
        {
            Debug.Log("Can be overridden in child clases to implement their own game over/death");
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }

    public virtual void DestroyObj()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (maxHealth <= 0)
        {
            maxHealth = 2;
        }

        health = maxHealth;
    }
}
