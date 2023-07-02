using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody PlayerRigidbody;
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Bullet bullet;
    [SerializeField] Text[] clock;
    [SerializeField] GameObject output;
    [SerializeField] GameObject canvasForEndGame;
    [SerializeField] GameObject score;
    [SerializeField] playSound fireSound;
    [SerializeField] playSound powerupSound;
    [SerializeField] Text TimeClock;
    [SerializeField] Text DMGText;
    AudioSource audio = null;
    const float timeToFire = -100f, hardFR = 6f, normalFR = 10f, minigun = 40;
    private bool debugmode = false;
    private Vector3 MAX_VELOCITY = Vector3.zero;
    public static float fireRate = 1f;
    public static int difficultyLevel;
    //private float MAX_BORDER = 375f;
    private float gunCoolDown = -100f;
    public static float sec = 0, min = 0;
    bool invulnerable = false;
    bool threeShot = false;
    bool playingWithKey = false;
    float[] multcounter = {0, 0, 0, 0, 0, 0};

    public void readDifficulty()
    {
        StreamReader sr = new StreamReader("Difficulty.txt");
        string str = sr.ReadLine();
        sr.Close();
        difficultyLevel = int.Parse(str); 
    }

    public void resetClock()
    {
        for(int i = 0; i < 6; i++)
        {
            multcounter[i] = 0;
            clock[i].text = "";
        }
        TimeClock.text = "0:00";
        min = 0;
        sec = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        audio.Play();
        fireRate = normalFR;
        readDifficulty();
        if(difficultyLevel == 3)
        {
            fireRate = hardFR;
        }
        resetClock();
        MAX_VELOCITY.Set(75000 * Time.deltaTime, 0, 0);
    }

    public void setVolume(int volume)
    {
        audio.volume = volume;
        fireSound.GetComponent<AudioSource>().volume = volume;
        powerupSound.GetComponent<AudioSource>().volume = volume;
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

    public String sendRecord()
    {
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
        return rcv;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!invulnerable)
            {
                audio.Stop();
                print("game ended");
                Score.setMult(1);
                if (difficultyLevel == 3)
                {
                    setFireRate(hardFR);
                }
                else
                {
                    setFireRate(normalFR);
                }
                BallMovement.timestop = false;
                BallMovement.bombed = false;
                setTwoShot(false);
                String rcv = sendRecord();
                if (rcv.Equals("new record"))
                {
                    canvasForEndGame.SetActive(true);
                    output.GetComponent<TextMeshProUGUI>().text = "new record " + Score.getScore();
                }
                else
                {
                    canvasForEndGame.SetActive(true);
                    output.GetComponent<TextMeshProUGUI>().text = "your record " + Score.getScore();
                }
                score.GetComponent<Score>().endgameReset();
                resetClock();
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.name.StartsWith("threeshot"))
        {
            powerupSound.GetComponent<AudioSource>().Play();
            setTwoShot(true);
            multcounter[0] = -5f;
            clock[0].text = "Three shot active";
        }
        if (collision.gameObject.name.StartsWith("shield"))
        {
            powerupSound.GetComponent<AudioSource>().Play();
            setInvulnerable(true);
            multcounter[1] = -5f;
            clock[1].text = "Invulnerbility active";
        }
        if (collision.gameObject.name.StartsWith("bomb"))
        {
            powerupSound.GetComponent<AudioSource>().Play();
            BallMovement.bombed = true;
            multcounter[2] = -1f;
            clock[2].text = "Bomb Droped";
            print("bomb");
        }
        if (collision.gameObject.name.StartsWith("timestop"))
        {
            powerupSound.GetComponent<AudioSource>().Play();
            BallMovement.timestop = true;
            multcounter[3] = -5f;
            clock[3].text = "Tick tock";
            print("timestop");
        }
        if (collision.gameObject.name.StartsWith("multiply"))
        {
            powerupSound.GetComponent<AudioSource>().Play();
            print("mult");
            Score.setMult(2);
            clock[4].text = "Points are doubled";
            multcounter[4] = -5f;
        }
        if (collision.gameObject.name.StartsWith("firerate"))
        {
            powerupSound.GetComponent<AudioSource>().Play();
            print("firerate");
            clock[5].text = "Minigun active";
            setFireRate(minigun);
            multcounter[5] = -5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sec += Time.deltaTime;
        if(sec > 60)
        {
            BallMovement.increaseDMG();
        }
        min += (int)(sec / 60);
        sec -= ((int)sec / 60) * 60;
        TimeClock.text = ((int)min).ToString() + ":" + (sec < 10? "0": "") + ((int)sec).ToString();
        if (!playingWithKey)
        {
            PlayerTransform.position = new Vector3(Math.Max(-470f, Math.Min(280, Input.mousePosition.x - 480f)), PlayerTransform.position.y, PlayerTransform.position.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playingWithKey = true;
            if (PlayerRigidbody.velocity.x > -MAX_VELOCITY.x)
            {
                if (PlayerRigidbody.velocity.x > 30000 * Time.deltaTime)
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
            playingWithKey = true;
            if (PlayerRigidbody.velocity.x < MAX_VELOCITY.x)
            {
                if (PlayerRigidbody.velocity.x < -30000 * Time.deltaTime)
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
        for (int i = 0; i < 6; i++)
        {
            if (multcounter[i] < 0)
            {
                multcounter[i] += Time.deltaTime;
                if (multcounter[i] >= 0)
                {
                    clock[i].text = "";
                    if(i == 4)
                    {
                        Score.setMult(1);
                    }
                    if(i == 5)
                    {
                        if (difficultyLevel == 3)
                        {
                            setFireRate(hardFR);
                        }
                        else
                        {
                            setFireRate(normalFR);
                        }
                    }
                    if(i == 1)
                    {
                        setInvulnerable(false);
                    }
                    if(i == 3)
                    {
                        BallMovement.timestop = false;
                    }
                    if(i == 2)
                    {
                        BallMovement.bombed = false;
                    }

                    if(i == 0)
                    {
                        setTwoShot(false);
                    }

                }
            }
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton((int)MouseButton.Left))
        {
            if (Input.GetMouseButton((int)MouseButton.Left))
            {
                playingWithKey = false;
            }
            else
            {
                playingWithKey = true;
            }
            if (gunCoolDown >= 0)
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
                fireSound.GetComponentInParent<AudioSource>().Play();
                gunCoolDown = timeToFire;
            }
        }
        if (gunCoolDown < 0)
        {
            gunCoolDown += fireRate;
        }
        if (debugmode)
        {
            invulnerable = true;
        }
        print("firerate:" + fireRate);
        DMGText.text = "Your Damage is " + BallMovement.DMG.ToString();
    }
}
