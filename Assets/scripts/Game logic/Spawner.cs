using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Balls;
    [SerializeField] Transform spawnerTransform;
    int index = 0;
    float countdown = 0;
    const int MAX_BALLS = 5;
    private int balls = 0;

    public void increaseBalls()
    {
        balls++;
    }
    public void DecreaseBalls()
    {
        balls--;
    }
    // Start is called before the first frame update
    void Start()
    {
        balls = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = 3;
            if(balls < MAX_BALLS)
            {
                Instantiate(Balls[index++ % 3], spawnerTransform);
                increaseBalls();
            }
        }
    }
}
