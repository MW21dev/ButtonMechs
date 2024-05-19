using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : AbilityButtonScript
{
    
    
    public override void UseAbility(PlayerStats player)
    {
        if (player.transform.eulerAngles.z == 0)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1f);
        }
        else if (player.transform.eulerAngles.z == 90f)
        {
            player.transform.position = new Vector2(player.transform.position.x - 1f, player.transform.position.y);
        }
        else if (player.transform.eulerAngles.z == 270)
        {
            player.transform.position = new Vector2(player.transform.position.x +1f, player.transform.position.y);
        }
        else if (player.transform.eulerAngles.z == 180)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1f);
        }
    }
}
