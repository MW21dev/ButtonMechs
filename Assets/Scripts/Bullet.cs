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
    public bool cursed;

    public AbilityButtonScript curseButton;

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

            if (cursed && curseButton != null)
            {
                var cButton = Instantiate(curseButton);
                cButton.transform.SetParent(GameManager.Instance.deck.transform, false);
                GameManager.Instance.UpdateDeck();
            }

            playerStats.GetHit(1);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            ObjectScript objectScript = collision.gameObject.GetComponent<ObjectScript>();
            objectScript.GetHit(1);
            Destroy(gameObject);
        }
    }
}
