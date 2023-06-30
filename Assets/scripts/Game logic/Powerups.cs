using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField] Transform powerupTransform;
    [SerializeField] Rigidbody powerupRigidbody;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        powerupRigidbody.AddForce(Random.Range(-10000, 10000), 10000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        powerupRigidbody.AddForce(0, -100, 0);
    }
}
