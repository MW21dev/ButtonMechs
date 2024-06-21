using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MapPickUp
{
    public SpriteRenderer spriteRenderer;
    public Sprite mineOff;
    public Sprite mineOn;
    public Sprite explode;

    public int repeats = 4;
    public int damage = 1;
    public override void UseAbility(Collider2D collison)
    {
        StartCoroutine(Explode(collison));
    }

    public void ExplodeDestroy()
    {
        Destroy(gameObject);
    }

    public IEnumerator Explode(Collider2D collision)
    {
        if(collision.gameObject.tag != "Ground")
        {
            for (int i = 0; i < repeats; i++)
            {
                if (spriteRenderer.sprite == mineOff && i != repeats - 1)
                {
                    spriteRenderer.sprite = mineOn;
                }
                else if (spriteRenderer.sprite == mineOn && i != repeats - 1)
                {
                    spriteRenderer.sprite = mineOff;
                }
                else if (i == repeats - 1)
                {
                    spriteRenderer.sprite = explode;
                    SoundManager.Instance.PlayUISound(5);

                    Invoke("ExplodeDestroy", 0.2f);

                    if (isOnTile)
                    {
                        if (collision.gameObject.tag == "Enemy")
                        {
                            collision.gameObject.GetComponent<EnemyBase>().GetHit(damage);
                        }
                        else if (collision.gameObject.tag == "Player")
                        {
                            collision.gameObject.GetComponent<PlayerStats>().GetHit(damage);
                        }
                    }


                }

                yield return new WaitForSeconds(0.5f);
            }
        }
        
        
    }
}
