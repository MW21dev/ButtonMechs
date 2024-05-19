using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    
    public bool isEnemyTurn = false;
    public int enemyActions;
    public int maxEnemyActions;
    public int enemyAbilitiesCount;
    public GameObject shotPoint;
    public GameObject bullet;

    private void Update()
    {
        if (GameManager.Instance.enemyTurn)
        {
            isEnemyTurn = true;
            if (isEnemyTurn)
            {
                DoAction();
                isEnemyTurn = false;
            }
        }

        if (GameManager.Instance.playerTurn)
        {
            enemyActions = maxEnemyActions;
        }
    }

    public void DoAction()
    {
        if(enemyActions > 0)
        {
            int rnd = Random.Range(0, enemyAbilitiesCount - 1);
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
            Invoke("DoAction", 0.2f);

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
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 90f);
    }

    public void Shot()
    {
        Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
    }
}
