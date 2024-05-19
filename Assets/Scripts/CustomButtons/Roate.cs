using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roate : AbilityButtonScript
{
    public bool right;
    public bool left;
    public override void UseAbility(PlayerStats player)
    {
        if(right)
        {
            if (player.transform.eulerAngles.z == 0)
            {
                player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 270f);

            }
            else if (player.transform.eulerAngles.z == 90f)
            {
                player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 0f);
            }
            else if (player.transform.eulerAngles.z == 270)
            {
                player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 180f);
            }
            else if (player.transform.eulerAngles.z == 180)
            {
                player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 90f);
            }
        }
        else if(left)
        {
            if (player.transform.eulerAngles.z == 0)
            {
                player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 90f);

            }
            else if (player.transform.eulerAngles.z == 90f)
            {
                player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 180f);
            }
            else if (player.transform.eulerAngles.z == 270)
            {
                player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 0f);
            }
            else if (player.transform.eulerAngles.z == 180)
            {
                player.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 270f);
            }
        }

    }
}
