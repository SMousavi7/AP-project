using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] Rigidbody PlayerRigidbody; 
	[SerializeField] Transform PlayerTransform;
	[SerializeField] Bullet bullet;
	private Vector3 MAX_VELOCITY = Vector3.zero;
	public static float fireRate = 0.1f; 
	private float MAX_BORDER = 375f;
	private float gunCoolDown = 0f;
	bool invulnerable = false;
	bool threeShot = false;
	float multcounter = 0;
    // Start is called before the first frame update
    void Start()
	{
		MAX_VELOCITY.Set(750, 0, 0);
	}

	public void setFireRate(float fireRate)
	{
		PlayerMovement.fireRate = fireRate;
	}

	public void setTwoShot(bool twoShot)
	{
		this.threeShot = twoShot;
	}

	public void setInvulnerable(bool able)
	{
		invulnerable = able;
	}


    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.CompareTag("Enemy"))
		{
			if (!invulnerable)
			{
				Destroy(this.gameObject);
				print("game ended");
			}
		}
		if (collision.gameObject.name.Equals("shield1"))
		{

		}
        if (collision.gameObject.name.Equals("shield2"))
        {

        }
        if (collision.gameObject.name.Equals("invulnerbility"))
        {

        }
        if (collision.gameObject.name.Equals("bomb"))
        {
			
        }
        if (collision.gameObject.name.Equals("timestop"))
        {

        }
        if (collision.gameObject.name.Equals("multiply"))
        {
			Score.setMult(2);
			multcounter = -30f;
        }
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
				if(PlayerRigidbody.velocity.x > 300)
				{
                    PlayerRigidbody.AddForce(-100000 * (PlayerRigidbody.velocity.x / 150.0f) * Time.deltaTime, 0, 0);
                }
                else
				{
					PlayerRigidbody.AddForce(-50000 * Time.deltaTime, 0, 0);
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
				if (PlayerRigidbody.velocity.x < -300)
				{
					PlayerRigidbody.AddForce(100000 * (PlayerRigidbody.velocity.x / -150.0f) * Time.deltaTime, 0, 0);
				}
				else
				{
					PlayerRigidbody.AddForce(50000 * Time.deltaTime, 0, 0);
                }
            }
		}
		else
		{
			PlayerRigidbody.velocity = PlayerRigidbody.velocity * 0.5f;
		}
		PlayerRigidbody.velocity.Normalize();
		if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton((int)MouseButton.Left))
		{
			if(gunCoolDown >= 0)
			{
				Vector3 pos = Vector3.zero;
				Quaternion rot = Quaternion.identity;
				pos.Set(PlayerTransform.position.x, -156f, -83f);
				Instantiate(bullet, pos, rot);
				if (threeShot)
				{
					pos.Set(PlayerTransform.position.x + 20f, -210f, -83f);
					Instantiate(bullet, pos, rot);
                    pos.Set(PlayerTransform.position.x - 20f, -210f, -83f);
                    Instantiate(bullet, pos, rot);
                }
				gunCoolDown = -fireRate;
			}
		}
		if(gunCoolDown < 0)
		{
			gunCoolDown += Time.deltaTime;
		}
		if(multcounter < 0)
		{
			multcounter += Time.deltaTime;
			if(multcounter >= 0) 
			{
				Score.setMult(1);
			}
		}
    }
}
