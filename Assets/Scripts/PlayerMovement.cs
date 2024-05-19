using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    
    public SpriteRenderer playerSpriteRenderer;
    public CircleCollider2D playerCollider;
    public Rigidbody2D playerRigidbody;

    public Transform shotPoint;

    public Vector3 currentRotation;
    public Vector3 angleToRotate;
    
    public void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerCollider = GetComponent<CircleCollider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        

        var pos = transform.position;

        currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y % 360f, currentRotation.z % 360f);
        transform.eulerAngles = currentRotation;

        

        transform.position = pos;
    }

    private void Update()
    {
        

        var pos = transform.position;

        pos.x = Mathf.Clamp(transform.position.x, -9.49f, 0.6f);
        pos.y = Mathf.Clamp(transform.position.y, 0.5f, 9.49f);

        

        transform.position = pos;

        shotPoint.rotation = transform.rotation;

        

    }
    

   
}
