using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    
    public bool isEnemyTurn = false;
    public int enemyActions;
    public int maxEnemyActions;
    public int enemyAbilitiesCount;
    public bool isCounted;

    public GameObject shotPoint;
    public GameObject bullet;
    public GameObject explosion;

    private void Start()
    {
        var pos = transform.position;

        pos.x = Mathf.Clamp(transform.position.x, -9.49f, -0.5f);
        pos.y = Mathf.Clamp(transform.position.y, 0.49f, 9.49f);
    }

    private void Update()
    {
        if (GameManager.Instance.enemyTurn)
        {
            
        }

        if (GameManager.Instance.playerTurn)
        {
            
        }

        
    }

    public void DoAction()
    {
        if (GameManager.Instance.enemyTurn)
        {
            if (enemyActions > 0)
            {
                int rnd = Random.Range(0, enemyAbilitiesCount);
                switch (rnd)
                {
                    case 0:
                        Move();
                        break;
                    case 1:
                        Rotate();
                        break;
                    case 2:
                        Shot();
                        break;
                }

                enemyActions -= 1;
                GameManager.Instance.maxEnemyActions -= 1;
                Invoke("DoAction", 0.2f);

            }

            
        }
   
    }


    public void Move()
    {
        if (transform.eulerAngles.z == 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 1f);

        }
        else if (transform.eulerAngles.z == 90f)
        {
            transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
        }
        else if (transform.eulerAngles.z == 270)
        {
            transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
        }
        else if (transform.eulerAngles.z == 180)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
        }
    }

    public void Rotate()
    {
        int rnd = Random.Range(0, 1);

        if (rnd == 0)
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 90f);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90f);
        }
    }

    public void Shot()
    {
        Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
    }

    public void GetHit(int damage)
    {
        
        var explosionPrefab = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explosionPrefab, 0.2f);
        GameManager.Instance.enemies.Remove(gameObject);
        Destroy(gameObject, 0.2f);

    }
}
