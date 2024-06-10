using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotateAndShot : EnemyAbilityButton
{
    
    public override void UseAbility(EnemyBase enemy)
    {
        int rnd = Random.Range(0, 2);

        if (rnd == 0)
        {
            enemy.transform.eulerAngles = new Vector3(enemy.transform.rotation.eulerAngles.x, enemy.transform.rotation.eulerAngles.y, enemy.transform.rotation.eulerAngles.z - 90f);
        }
        else
        {
            enemy.transform.eulerAngles = new Vector3(enemy.transform.rotation.eulerAngles.x, enemy.transform.rotation.eulerAngles.y, enemy.transform.rotation.eulerAngles.z + 90f);
        }

        Instantiate(enemy.bullet, enemy.shotPoint.transform.position, enemy.shotPoint.transform.rotation);

        if(enemy.shotPoint2 != null)
        {
            Instantiate(enemy.bullet, enemy.shotPoint2.transform.position, enemy.shotPoint2.transform.rotation);

        }

    }
}
