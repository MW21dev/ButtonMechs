using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleQuickShot : AbilityModule
{
    public GameObject bullet;
    public GameObject shotPoint = null;

    private void Start()
    {
        Flip.flipUsed += Shot;

        description.text = $"{abilityName}" + "\n" + "\n " + $"{abilityDescription}";
        abilityText.SetActive(false);

    }

    private void OnDestroy()
    {
        Flip.flipUsed -= Shot;
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

    void Shot()
    {
        if (inShop)
        {
            return;
        }

        shotPoint = GameObject.Find("ShotPoint");
        Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);

        SoundManager.Instance.PlayUISound(3);
    }
}
