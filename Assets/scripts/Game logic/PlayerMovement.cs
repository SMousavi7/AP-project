using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody PlayerRigidbody; 
	public Transform PlayerTransform;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			if(PlayerRigidbody.velocity.x > -100)
			{ 
				if(PlayerRigidbody.velocity.x > 10)
				{
                    PlayerRigidbody.AddForce(-1000 * (PlayerRigidbody.velocity.x / 15.0f) * Time.deltaTime, 0, 0);
                }
                else
				{
					PlayerRigidbody.AddForce(-1500 * Time.deltaTime, 0, 0);
				}
			}
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			if (PlayerRigidbody.velocity.x < 100)
			{
				if (PlayerRigidbody.velocity.x < -10)
				{
					PlayerRigidbody.AddForce(1000 * (PlayerRigidbody.velocity.x / -15.0f) * Time.deltaTime, 0, 0);
				}
				else
				{
					PlayerRigidbody.AddForce(1500 * Time.deltaTime, 0, 0);
				}
			}
		}
	}
}
