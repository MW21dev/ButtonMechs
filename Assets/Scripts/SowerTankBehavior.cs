using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SowerTankBehavior : EnemyBase
{
    public bool moved;
    public override void EnemyNextAction()
    {
        if (enemyActions > 0)
        {
            if (canMove && !moved)
            {
                EnemyAbilityButton ab1 = enemyability1.GetComponent<EnemyAbilityButton>();
                ab1.UseAbility(this);
                Debug.Log("Move Forwad");
                moved = true;
            }
            else if (!canMove && !moved)
            {
                EnemyAbilityButton ab2 = enemyability2.GetComponent<EnemyAbilityButton>();
                ab2.UseAbility(this);
                Debug.Log("Rotate");
            }
            else if (moved)
            {
                EnemyAbilityButton ab3 = enemyability3.GetComponent<EnemyAbilityButton>();
                ab3.UseAbility(this);
                Debug.Log("Place Mine");
            }

            enemyActions -= 1;
            GameManager.Instance.maxEnemyActions -= 1;
            Invoke("EnemyNextAction", 0.5f);

            if (enemyActions == 0)
            {
                ResetMoved();
            }
        }
    }

    void ResetMoved()
    {
        moved = false;
    }
}
