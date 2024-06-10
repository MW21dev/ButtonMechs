using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTankBehavior : EnemyBase
{
    
    public override void EnemyNextAction()
    {
        if (enemyActions > 0)
        {
            EnemyAbilityButton ab1 = enemyability1.GetComponent<EnemyAbilityButton>();
            ab1.UseAbility(this);
            Debug.Log("RotateAndShot");

            

            enemyActions -= 1;
            GameManager.Instance.maxEnemyActions -= 1;
            
        }
    }

   
}
