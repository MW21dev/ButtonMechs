using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursedTankBehavior : EnemyBase
{
    private int rnd;
    public override void EnemyNextAction()
    {
        if (enemyActions > 0)
        {
            RerollAction();

            if (isFacingPlayer)
            {
                EnemyAbilityButton ab3 = enemyability3.GetComponent<EnemyAbilityButton>();
                ab3.UseAbility(this);
                Debug.Log("Shot");
            }
            else if (canMove && !isFacingPlayer)
            {

                switch (rnd)
                {
                    case 0:
                        EnemyAbilityButton ab1 = enemyability1.GetComponent<EnemyAbilityButton>();
                        ab1.UseAbility(this);
                        Debug.Log("Move Forwad");
                        break;
                    case 1:
                        EnemyAbilityButton ab2 = enemyability2.GetComponent<EnemyAbilityButton>();
                        ab2.UseAbility(this);
                        Debug.Log("Rotate");
                        break;
                }

            }
            else if (!canMove && !isFacingPlayer)
            {
                EnemyAbilityButton ab2 = enemyability2.GetComponent<EnemyAbilityButton>();
                ab2.UseAbility(this);
                Debug.Log("Rotate");
            }

            enemyActions -= 1;
            GameManager.Instance.maxEnemyActions -= 1;
            Invoke("EnemyNextAction", 0.5f);
        }
    }

    void RerollAction()
    {
        rnd = Random.Range(0, 2);
        Debug.Log(rnd);
    }
}
