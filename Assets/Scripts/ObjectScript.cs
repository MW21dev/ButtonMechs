using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public string objName;
    public int objMaxHealth;
    public int objCurrentHealth;
    public bool destructable;
    public bool movable;
    public bool mapBorder;


    public GameObject explosion;

    public void GetHit(int damage)
    {
        if (!mapBorder)
        {
            objCurrentHealth -= damage;
            var explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionPrefab, 0.2f);
        }
        
        

        if (objCurrentHealth == 0 && destructable)
        {
            Invoke("Dead", 0.1f);
        }
    }

    void Dead()
    {
       Destroy(gameObject);
    }
}
