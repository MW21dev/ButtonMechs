using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public LayerMask mask;
    
    public SpriteRenderer playerSpriteRenderer;
    public CircleCollider2D playerCollider;
    public Rigidbody2D playerRigidbody;

    public Transform shotPoint;

    public Vector3 currentRotation;
    public Vector3 rayCheck;

    public bool canMove;
    
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

        
        transform.eulerAngles = currentRotation;

        

        transform.position = pos;
    }

    private void Update()
    {

        var pos = transform.position;

        pos.x = Mathf.Clamp(transform.position.x, -9.49f, -0.5f);
        pos.y = Mathf.Clamp(transform.position.y, 0.49f, 9.49f);

        

        transform.position = pos;

        shotPoint.rotation = transform.rotation;

        currentRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        if (transform.eulerAngles.z == 0)
        {
            rayCheck = Vector3.up;
        }
        else if (transform.eulerAngles.z == 90f)
        {
            rayCheck = Vector3.left;
        }
        else if (transform.eulerAngles.z == 270)
        {
            rayCheck = Vector3.right;

        }
        else if (transform.eulerAngles.z == 180)
        {
            rayCheck = Vector3.down;

        }


    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(shotPoint.position, rayCheck);

        Debug.DrawRay(shotPoint.position, rayCheck, Color.red);

        if (hit.collider.gameObject.tag == "Border" && hit.collider.gameObject.tag == "Ground")
        {
            Debug.Log("cant move");
            canMove = false;
        }
        else if(hit.collider.gameObject.tag != "Border" || hit.collider.gameObject.tag != "Enemy")
        {
            Debug.Log("can move");
            canMove = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().GetHit(1000);
        }
    }

    

}
