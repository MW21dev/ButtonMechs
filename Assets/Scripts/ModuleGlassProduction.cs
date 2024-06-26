using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModuleGlassProduction : AbilityModule
{
    public AbilityButtonScript[] glassButtons;
    
    private void Start()
    {
        LevelManager.Instance.OnLaunchLevel += CreateGlass;

        description.text = $"{abilityName}" + "\n" + "\n " + $"{abilityDescription}";
        abilityText.SetActive(false);

    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnLaunchLevel -= CreateGlass;

    }


    public new void Update()
    {
        

        if (isHovering)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 0)
        {
            abilityText.SetActive(true);

            if (inShop)
            {
                abilityText.transform.localPosition = new Vector3(abilityText.transform.localPosition.x, 15f, abilityText.transform.localPosition.z);
            }
            else
            {
                abilityText.transform.localPosition = new Vector3(abilityText.transform.localPosition.x, -75f, abilityText.transform.localPosition.z);
            }
        }

        if (!isHovering)
        {
            abilityText.SetActive(false);
        }
    }

    void CreateGlass()
    {
        if (!inShop)
        {
            int rnd = Random.Range(0, glassButtons.Length);

            var button = Instantiate(glassButtons[rnd].gameObject);
            button.transform.SetParent(GameManager.Instance.deck.transform, false);
            GameManager.Instance.UpdateDeck();
        }
    }
}
