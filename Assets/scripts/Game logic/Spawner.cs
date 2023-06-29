using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Balls;
    [SerializeField] Transform spawnerTransform;
    int index = 0;
    public static float BALLSPAWNRATE = 5;
    float countdown = 0;
    public static int MAX_BALLS = 3;
    private static int balls = 0;

    public static void increaseBalls()
    {
        balls++;
    }
    public static void DecreaseBalls()
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
            countdown = BALLSPAWNRATE;
            print(balls + " " + MAX_BALLS);
            if(balls < MAX_BALLS)
            {
                Instantiate(Balls[index++ % 3], spawnerTransform);
                increaseBalls();
            }
        }
    }
}
