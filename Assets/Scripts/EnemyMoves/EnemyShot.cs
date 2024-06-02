using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : EnemyAbilityButton
{
    public override void UseAbility(EnemyBase enemy)
    {
        Instantiate(enemy.bullet, enemy.shotPoint.transform.position, enemy.shotPoint.transform.rotation);
    }
}
