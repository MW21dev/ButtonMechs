using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePanel : MonoBehaviour
{
    private void Update()
    {
        if (transform.childCount > 0)
        {
            int childNmb = transform.childCount;
            for (int i = 0; i < childNmb; i++)
            {
                AbilityModule module = gameObject.transform.GetChild(i).GetComponent<AbilityModule>();
                module.inShop = false;
            }
        }
    }
}
