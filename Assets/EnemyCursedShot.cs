using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCursedShot : EnemyAbilityButton
{
    public override void UseAbility(EnemyBase enemy)
    {
        Instantiate(enemy.bullet, enemy.shotPoint.transform.position, enemy.shotPoint.transform.rotation);
        SoundManager.Instance.PlayUISound(4);
    }
}
