using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform bulletTransform;
    [SerializeField] Rigidbody bulletRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody.AddForce(0, 50000, 0);
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.CompareTag("Roof") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
