using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class Powerupspawner : MonoBehaviour
{
    [SerializeField] Transform powerupspawnerTransform;
    [SerializeField] GameObject[] powerup;
    public static float spawnRate = 5f;
    float counter = 0;

    public void spawnPowerup()
    {
        int chance = Random.Range(0, 100);
        if(chance < 20)
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            pos.Set(powerupspawnerTransform.position.x, powerupspawnerTransform.position.y, powerupspawnerTransform.position.z);
            Instantiate(powerup[0], pos, rot);
        }
        else if(chance < 40)
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            pos.Set(powerupspawnerTransform.position.x, powerupspawnerTransform.position.y, powerupspawnerTransform.position.z);
            Instantiate(powerup[1], pos, rot);

        }
        else if(chance < 60)
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            pos.Set(powerupspawnerTransform.position.x, powerupspawnerTransform.position.y, powerupspawnerTransform.position.z);
            Instantiate(powerup[2], pos, rot);

        }
        else if( chance < 80)
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            pos.Set(powerupspawnerTransform.position.x, powerupspawnerTransform.position.y, powerupspawnerTransform.position.z);
            Instantiate(powerup[3], pos, rot);

        }
        else
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            pos.Set(powerupspawnerTransform.position.x, powerupspawnerTransform.position.y, powerupspawnerTransform.position.z);
            Instantiate(powerup[4], pos, rot);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //read spawnrate from file
        counter = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if(counter < 0 )
        {
            spawnPowerup();
            counter = spawnRate;
        }
    }
}
