using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shotForce = 10f;
    public int damage;
    Rigidbody2D rb;

    public bool enemyBullet;
    public bool playerbullet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 3f);
        
    }
    private void Update()
    {
        rb.velocity = transform.up * shotForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && playerbullet)
        {
            EnemyBase enemyBase = collision.gameObject.GetComponent<EnemyBase>();
            damage = PlayerStats.Instance.playerDamage;
            enemyBase.GetHit(damage);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Player") && enemyBullet)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            
            playerStats.GetHit(0);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
