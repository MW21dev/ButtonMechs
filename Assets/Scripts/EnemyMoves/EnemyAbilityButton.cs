using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyAbilityButton : MonoBehaviour
{
    [TextArea]
    public string abilityDescription;

    public Image abilityImage;
    public string abilityName;

    

    private void Start()
    {
        
    }

    public virtual void UseAbility(EnemyBase enemy)
    {
        
    }

}
