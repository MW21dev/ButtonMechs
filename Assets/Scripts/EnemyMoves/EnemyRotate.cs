using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : EnemyAbilityButton
{
    public override void UseAbility(EnemyBase enemy)
    {
        int rnd = Random.Range(0, 1);

        if (rnd == 0)
        {
            enemy.transform.eulerAngles = new Vector3(enemy.transform.rotation.eulerAngles.x, enemy.transform.rotation.eulerAngles.y, enemy.transform.rotation.eulerAngles.z - 90f);
        }
        else
        {
            enemy.transform.eulerAngles = new Vector3(enemy.transform.rotation.eulerAngles.x, enemy.transform.rotation.eulerAngles.y, enemy.transform.rotation.eulerAngles.z + 90f);
        }
    }
}
