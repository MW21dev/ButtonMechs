using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shotForce = 10f;
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
            GameManager.Instance.enemies.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Player") && enemyBullet)
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
