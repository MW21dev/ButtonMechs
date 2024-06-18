using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuExit : AbilityButtonScript
{
    private TurnOnAnimation animation;
    
    private void Awake()
    {
        animation = GameObject.FindAnyObjectByType<TurnOnAnimation>();
    }

    public override void UseMenuAbility()
    {
        animation.gameObject.SetActive(true);
        animation.LaunchTurnOff();
    }
}
