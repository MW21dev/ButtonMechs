using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance;
	public float rayRange = 0.5f;

	public LayerMask mask;
	
	public SpriteRenderer playerSpriteRenderer;
	public CircleCollider2D playerCollider;
	public Rigidbody2D playerRigidbody;

	public Transform shotPoint;

	public Vector3 currentRotation;
	public Vector2 rayCheck;

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

		shotPoint.position = Vector3Extension.AsVector2(shotPoint.transform.position);
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
			rayCheck = Vector2.up;
		}
		else if (transform.eulerAngles.z == 90f)
		{
			rayCheck = Vector2.left;

		}
		else if (transform.eulerAngles.z == 270)
		{
			rayCheck = Vector2.right;

		}
		else if (transform.eulerAngles.z == 180)
		{
			rayCheck = Vector2.down;

		}


	}

	private void FixedUpdate()
	{
		Vector2 shotPP = Vector3Extension.AsVector2 (shotPoint.transform.position);

		RaycastHit2D hitItem = Physics2D.Raycast(shotPP, rayCheck, rayRange, mask);

		Debug.DrawRay(shotPP, rayCheck, Color.red);


		if (hitItem)
		{
			Debug.Log(hitItem.collider.gameObject.name);
			if (hitItem.collider.gameObject.tag == "Border")
			{
				Debug.Log("cant move");
				canMove = false;
			}
		}
		else
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
