using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    [SerializeField] Rigidbody ballRigidbody;
    [SerializeField] int hp = 20;
    [SerializeField] Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody.AddForce(Random.Range(-10000, 10000), 0, 0);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp--;
            if (hp <= 0)
            {
                Destroy(this.gameObject);
                spawner.DecreaseBalls();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        ballRigidbody.AddForce(0, -100, 0);
    }
}
