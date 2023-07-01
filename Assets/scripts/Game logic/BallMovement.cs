using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BallMovement : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    [SerializeField] Rigidbody ballRigidbody;
    [SerializeField] int hp;
    [SerializeField] GameObject smallBall, mediumBall;
    [SerializeField] TextMeshPro hpText;
    public static int DMG = 1; 
    public static int gravity = -40000;
    public static int difficulty = 1;
    public static bool timestop = false;
    public static bool bombed = false;
    private int initialhp;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerMovement.difficultyLevel;
        ballRigidbody.AddForce(Random.Range(-10, 10) * 1000, 10000, 0);
        initialhp = hp * difficulty + (int)PlayerMovement.min * 2;
        hpText.text = initialhp.ToString();
    }
    public static void increaseDMG()
    {
        DMG++;
    }

    public static void resetDMG()
    {
        DMG = 1;
    }
    private void OnEnable()
    {
        initialhp = hp * difficulty + (int)PlayerMovement.min * 2;
    }
    public void setHp(int hp)
    {
        this.hp = hp;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            ballRigidbody.AddForce(0, 1000, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            ballRigidbody.AddForce(0, 40000, 0);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            initialhp-=DMG;
            Vector3 newsize = new Vector3(ballTransform.localScale.x * 1.02f, ballTransform.localScale.y * 1.02f, ballTransform.localScale.z * 1.02f);
            ballTransform.localScale = newsize;
            if(hp == 3)
            {
                if(initialhp <= 0)
                {
                    Spawner.DecreaseBalls();
                    Score.addScore(hp * difficulty);
                    Destroy(this.gameObject);
                }
            }
            else if (initialhp <= 0)
            {
                Vector3 pos = Vector3.zero;
                pos.Set(ballTransform.position.x + 20f, ballTransform.position.y, ballTransform.position.z);
                Quaternion rot = Quaternion.identity;
                if(hp == 6)
                {
                    Instantiate(smallBall, pos, rot);
                    pos.Set(ballTransform.position.x - 20f, ballTransform.position.y, ballTransform.position.z);
                    Instantiate(smallBall, pos, rot);
                    Spawner.increaseBalls();
                    Spawner.increaseBalls();
                }
                else if (hp == 12)
                {
                    Instantiate(mediumBall, pos, rot);
                    pos.Set(ballTransform.position.x - 20f, ballTransform.position.y, ballTransform.position.z);
                    Instantiate(mediumBall, pos, rot);
                    Spawner.increaseBalls();
                    Spawner.increaseBalls();
                }
                Destroy(this.gameObject);
                Score.addScore(hp);
                Spawner.DecreaseBalls();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        hpText.text = initialhp.ToString();
        if (bombed || ballTransform.position.y < -600)
        {
            Score.addScore(hp);
            Destroy(this.gameObject);
            Spawner.DecreaseBalls();
        }
        else if (timestop)
        {
            ballRigidbody.isKinematic = true;
        }
        else if(ballRigidbody.isKinematic == false)
        {
            ballRigidbody.AddForce(0, gravity * Time.deltaTime, 0);
        }
        else
        {
            ballRigidbody.isKinematic = false;
            ballRigidbody.AddForce(0, gravity * Time.deltaTime, 0);
        }
    }
}