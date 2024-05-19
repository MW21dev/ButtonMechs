using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GroundTile : MonoBehaviour
{
    public Sprite[] groundTile;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        int rn = Random.Range(0, groundTile.Length - 1);

        spriteRenderer.sprite = groundTile[rn];
    }
}
