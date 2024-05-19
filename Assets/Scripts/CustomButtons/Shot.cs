using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : AbilityButtonScript
{
    public GameObject bullet;
    public GameObject shotPoint;

    
    public override void UseAbility(PlayerStats player)
    {
        shotPoint = GameObject.Find("ShotPoint");
        Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
    }
}
