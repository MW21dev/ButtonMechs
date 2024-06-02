using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffectScipt : MonoBehaviour
{
    public Material material;

    public float screenSpeed = 0.001f;

    private void Update()
    {
        material.mainTextureOffset = new Vector2(material.mainTextureOffset.x , material.mainTextureOffset.y + screenSpeed * Time.deltaTime);

        if(material.mainTextureOffset.y >= 2)
        {
            material.mainTextureOffset = new Vector2(material.mainTextureOffset.x, 0f);
        }
    }
}
