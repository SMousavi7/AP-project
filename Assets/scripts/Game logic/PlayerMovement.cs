using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] Rigidbody PlayerRigidbody; 
	[SerializeField] Transform PlayerTransform;
	[SerializeField] Bullet bullet;
	[SerializeField] Text clock;
	private Vector3 MAX_VELOCITY = Vector3.zero;
	public static float fireRate = 0.1f;
	public static int difficultyLevel;
	private float MAX_BORDER = 375f;
	private float gunCoolDown = 0f;
	bool invulnerable = false;
	bool threeShot = false;
	float multcounter = 0;
    // Start is called before the first frame update
    void Start()
	{
		StreamReader sr = new StreamReader("Difficulty.txt");
		string str = sr.ReadLine();
		sr.Close();
		difficultyLevel = int.Parse(str);
		if( difficultyLevel == 3)
		{
			fireRate = 0.15f;
		}
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
		print(collision.gameObject.name);
		if (collision.gameObject.CompareTag("Enemy"))
		{
			if (!invulnerable)
			{
				Destroy(this.gameObject);
				print("game ended");

                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);

                string toSend = "" + Score.getScore();
                string select = "6";
                byte[] temp = System.Text.Encoding.UTF8.GetBytes(select);
                clientSocket.Send(temp);
                int toSendLen = System.Text.Encoding.ASCII.GetByteCount(toSend);
                byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(toSend);
                byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
                clientSocket.Send(toSendLenBytes);
                clientSocket.Send(toSendBytes);

				StreamReader sr = new StreamReader("temp_username.txt");
				string str = sr.ReadLine();
				sr.Close();

                int toSendLen1 = System.Text.Encoding.ASCII.GetByteCount(str);
                byte[] toSendBytes1 = System.Text.Encoding.ASCII.GetBytes(str);
                byte[] toSendLenBytes1 = System.BitConverter.GetBytes(toSendLen1);
                clientSocket.Send(toSendLenBytes1);
                clientSocket.Send(toSendBytes1);

                byte[] rcvLenBytes = new byte[4];
                clientSocket.Receive(rcvLenBytes);
                int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
                byte[] rcvBytes = new byte[rcvLen];
                clientSocket.Receive(rcvBytes);
                String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
				if(rcv.Equals("new record"))
				{
					print(rcv);
				}
				else
				{
					print(Score.getScore());
				}
            }
		}
        if (collision.gameObject.name.StartsWith("threeshot"))
        {
			setTwoShot(true);
			multcounter = -5f;
        }
        if (collision.gameObject.name.StartsWith("shield"))
        {
            setInvulnerable(true);
            multcounter = -5f;
        }
        if (collision.gameObject.name.StartsWith("invulnerbility"))
        {
			print("invulnerbility");
			setInvulnerable(true);
			multcounter = -5f;
        }
        if (collision.gameObject.name.StartsWith("bomb"))
        {
			BallMovement.bombed = true;
			multcounter = -1f;
			print("bomb");
        }
        if (collision.gameObject.name.StartsWith("timestop"))
        {
			BallMovement.timestop = true;
			multcounter = -6f;
			print("timestop");
        }
        if (collision.gameObject.name.StartsWith("multiply"))
        {
			print("mult");
			Score.setMult(2);
			multcounter = -5f;
        }
        if (collision.gameObject.name.StartsWith("firerate"))
        {
			print("firerate");
			setFireRate(0.05f);
            multcounter = -5f;
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
		clock.text = " ";
		if(multcounter < 0)
		{
			clock.text = "Power up remaining: " + ((int)-multcounter).ToString();
			multcounter += Time.deltaTime;
			if(multcounter >= 0) 
			{
				Score.setMult(1);
				if(difficultyLevel == 3)
				{
					setFireRate(0.15f);
				}
				else
				{
					setFireRate(0.1f);
				}
				setInvulnerable(false);
				BallMovement.timestop = false;
				BallMovement.bombed = false;
				setTwoShot(false);
			}
		}
    }
}
