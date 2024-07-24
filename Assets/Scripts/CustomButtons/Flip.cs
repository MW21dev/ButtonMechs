using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : AbilityButtonScript
{
    public static event Action flipUsed;

    public override void UseAbility(PlayerStats player)
    {
        if (player.transform.eulerAngles.z == 0f)
        {
            player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z + 180f);
        }
        else if (player.transform.eulerAngles.z == 90f)
        {
            player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z + 180);
        }
        else if (player.transform.eulerAngles.z == 270f)
        {
            player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z - 180f);
        }
        else if (player.transform.eulerAngles.z == 180f)
        {
            player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z - 180f);
        }

        flipUsed?.Invoke();
    }
}
