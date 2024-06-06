using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveForward : EnemyAbilityButton
{
    public override void UseAbility(EnemyBase enemy)
    {
        if (enemy.canMove)
        {
            if (enemy.transform.eulerAngles.z == 0)
            {
                enemy.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + 1f);

            }
            else if (enemy.transform.eulerAngles.z == 90f)
            {
                enemy.transform.position = new Vector2(enemy.transform.position.x - 1f, enemy.transform.position.y);
            }
            else if (enemy.transform.eulerAngles.z == 270)
            {
                enemy.transform.position = new Vector2(enemy.transform.position.x + 1f, enemy.transform.position.y);
            }
            else if (enemy.transform.eulerAngles.z == 180)
            {
                enemy.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y - 1f);
            }
        }
        
    }
}
