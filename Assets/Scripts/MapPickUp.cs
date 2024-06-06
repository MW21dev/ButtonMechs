using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPickUp : MonoBehaviour
{
    public bool isOnTile;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnTile = true;
        UseAbility(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnTile = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isOnTile = true;
    }

    public virtual void UseAbility(Collider2D collison)
    {

    }
}
