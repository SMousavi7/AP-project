using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    [SerializeField] Rigidbody ballRigidbody;
    [SerializeField] int hp;
    [SerializeField] GameObject smallBall, mediumBall;
    public static int gravity = -100;
    public static bool timestop = false;
    public static bool bombed = false;
    private int initialhp;
    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody.AddForce(Random.Range(-10000, 10000), 10000, 0);
        initialhp = hp;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ballRigidbody.AddForce(0, 38000, 0);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            initialhp--;
            if(hp == 3)
            {
                if(initialhp <= 0)
                {
                    Spawner.DecreaseBalls();
                    Score.addScore(hp);
                    Destroy(this.gameObject);
                }
            }
            else if (initialhp <= hp / 2)
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
                Score.addScore(hp / 2);
                Spawner.DecreaseBalls();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (bombed)
        {
            Destroy(this.gameObject);
        }
        else if (timestop)
        {
            ballRigidbody.velocity.Set(0, 0, 0);
        }
        else
        {
            ballRigidbody.AddForce(0, gravity, 0);
        }
    }
}