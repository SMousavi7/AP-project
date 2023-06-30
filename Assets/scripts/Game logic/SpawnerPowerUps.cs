using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPowerUps : MonoBehaviour
{
    [SerializeField] GameObject[] powerups;
    [SerializeField] Transform spawnerTransform;
    int index = 0;
    public static float SPAWNRATE = 30;
    float countdown = 0;
    Vector3 spawnPosition = Vector3.zero;
    Quaternion spawnRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition.Set(0, 230, -80);
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = SPAWNRATE;
            Instantiate(powerups[index], spawnPosition, spawnRotation);
            index = rand();
        }
    }

    int rand()
    {
        return 0;
    }
}
