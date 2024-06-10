using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaceMine : EnemyAbilityButton
{
    public override void UseAbility(EnemyBase enemy)
    {
        var mine = Instantiate(enemy.bullet2, enemy.shotPointSpecial.transform.position, enemy.shotPoint.transform.rotation);
    }

    
}
