using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GroundTile : MonoBehaviour
{
    public Sprite[] groundTile;

    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public bool empty;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        int rn = Random.Range(0, groundTile.Length - 1);

        spriteRenderer.sprite = groundTile[rn];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            empty = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            empty = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            empty = false;
        }
    }
}
