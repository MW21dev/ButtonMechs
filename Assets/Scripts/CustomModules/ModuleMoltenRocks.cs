using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMoltenRocks : AbilityModule
{
    bool isUsed;
    public List<ObjectScript> rocks;

    private void Start()
    {
        LevelManager.Instance.OnEndLevel += ClearList;
        LevelManager.Instance.MapCreated += StartCountingRocks;


        description.text = $"{abilityName}" + "\n" + "\n " + $"{abilityDescription}";
        abilityText.SetActive(false);
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnEndLevel -= ClearList;
        LevelManager.Instance.MapCreated -= StartCountingRocks;


    }


    private new void Update()
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

    IEnumerator CountRocks()
    {
        if (!inShop)
        {
            foreach (var Rock in GameObject.FindGameObjectsWithTag("Border"))
            {
                ObjectScript obj = Rock.GetComponent<ObjectScript>();

                if (!obj.mapBorder)
                {
                    rocks.Add(obj);
                }

                for (int i = 0; i < rocks.Count; i++)
                {
                    ObjectScript rock = rocks[i];
                    ChangeHealth(rock, 1);
                }

                yield return new WaitForSeconds(0.1f);

            }
        }

        
    }

    void StartCountingRocks()
    {
        if (!inShop && !isUsed)
        {
            StartCoroutine(CountRocks());
            isUsed = true;
        }
    }

    void ClearList()
    {
        rocks.Clear();
        isUsed = false;
    }

    void ChangeHealth(ObjectScript rock, int health)
    {
        rock.objCurrentHealth = health;
    }
}
