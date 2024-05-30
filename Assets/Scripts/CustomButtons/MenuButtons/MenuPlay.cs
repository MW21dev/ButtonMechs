using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlay : AbilityButtonScript
{
    public override void UseMenuAbility()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
