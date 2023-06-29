using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody PlayerRigidbody; 
	public Transform PlayerTransform;
	private Vector3 MAX_VELOCITY = Vector3.zero;
	private float MAX_BORDER = 375f;
	// Start is called before the first frame update
	void Start()
	{
		MAX_VELOCITY.Set(500, 0, 0);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			if(!(PlayerTransform.position.x > -MAX_BORDER))
            {
                PlayerRigidbody.velocity = PlayerRigidbody.velocity * 0.5f;
            }
            else if(PlayerRigidbody.velocity.x > -MAX_VELOCITY.x)
			{ 
				if(PlayerRigidbody.velocity.x > 100)
				{
                    PlayerRigidbody.AddForce(-70000 * (PlayerRigidbody.velocity.x / 150.0f) * Time.deltaTime, 0, 0);
                }
                else
				{
					PlayerRigidbody.AddForce(-30000 * Time.deltaTime, 0, 0);
				}
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			if(!(PlayerTransform.position.x < MAX_BORDER))
            {
                PlayerRigidbody.velocity = PlayerRigidbody.velocity * 0.5f;
            }
            else if (PlayerRigidbody.velocity.x < MAX_VELOCITY.x)
			{
				if (PlayerRigidbody.velocity.x < -100)
				{
					PlayerRigidbody.AddForce(70000 * (PlayerRigidbody.velocity.x / -150.0f) * Time.deltaTime, 0, 0);
				}
				else
				{
					PlayerRigidbody.AddForce(30000 * Time.deltaTime, 0, 0);
				}
			}
		}
		else
		{
			PlayerRigidbody.velocity = PlayerRigidbody.velocity * 0.5f;
		}
		PlayerRigidbody.velocity.Normalize();

    }
}
