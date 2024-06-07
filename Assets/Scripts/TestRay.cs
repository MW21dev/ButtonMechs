using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{
	public bool faceCheck;
	public LayerMask blek;

	private void Update()
	{
		faceCheck = Physics2D.Raycast(transform.position, Vector2.up, 10f, blek);
	}
}
