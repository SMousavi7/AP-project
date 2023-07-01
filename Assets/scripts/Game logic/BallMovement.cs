using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    [SerializeField] Rigidbody ballRigidbody;
    [SerializeField] int hp;
    [SerializeField] GameObject smallBall, mediumBall;
    [SerializeField] TextMeshPro hpText;
    public static int gravity = -40000;
    public static int difficulty = 1;
    public static bool timestop = false;
    public static bool bombed = false;
    private int initialhp;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerMovement.difficultyLevel;
        ballRigidbody.AddForce(Random.Range(-10000, 10000), 10000, 0);
        initialhp = hp * difficulty;
        hpText.text = initialhp.ToString();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            ballRigidbody.AddForce(0, 40000, 0);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            initialhp--;
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
                pos.Set(ballTransform.position.x, ballTransform.position.y, ballTransform.position.z);
                Quaternion rot = Quaternion.identity;
                if(hp == 6)
                {
                    Instantiate(smallBall, pos, rot);
                    Instantiate(smallBall, pos, rot);
                    Spawner.increaseBalls();
                    Spawner.increaseBalls();
                }
                else if (hp == 12)
                {
                    Instantiate(mediumBall, pos, rot);
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
        if (bombed)
        {
            Score.addScore(hp);
            Destroy(this.gameObject);
            Spawner.DecreaseBalls();
        }
        else if (timestop)
        {
            ballRigidbody.isKinematic = true;
        }
        else
        {
            ballRigidbody.isKinematic = false;
            ballRigidbody.AddForce(0, gravity * Time.deltaTime, 0);
        }
    }
}