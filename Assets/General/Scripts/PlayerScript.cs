using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour 
{
	public KeyCode moveUp, moveDown, moveLeft, moveRight;
	public float moveSpeed;

	void Start ()
	{
		moveSpeed /= 100;
	}

	void Update ()
	{
		if(Input.GetKey(moveUp))
		{
			transform.Translate(transform.up * moveSpeed);
		}

		else if(Input.GetKey(moveDown))
		{
			transform.Translate(-transform.up * moveSpeed);
		}

		else if(Input.GetKey(moveLeft))
		{
			transform.Translate(-transform.right * moveSpeed);
		}

		else if(Input.GetKey(moveRight))
		{
			transform.Translate(transform.right * moveSpeed);
		}
	}
}
