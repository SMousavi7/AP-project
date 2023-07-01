using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Balls;
    [SerializeField] Transform spawnerTransform;
    int index = 0;
    public static float BALLSPAWNRATE = 5;
    float countdown = 0;
    public static int MAX_BALLS = 3;
    private int difficultyLevel;
    private static int balls = 0;
    Vector3 spawnPosition = Vector3.zero;
    Quaternion spawnRotation = Quaternion.identity;

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
        //read BALLSPAWNRATE and MAXBALLS from filepublic void readDifficulty()
        StreamReader sr = new StreamReader("Difficulty.txt");
        string str = sr.ReadLine();
        sr.Close();
        difficultyLevel = int.Parse(str);
        
        if (difficultyLevel == 2)
        {
            BALLSPAWNRATE = 4;
            MAX_BALLS = 4;
        }
        if(difficultyLevel == 3)
        {
            BALLSPAWNRATE = 3;
            MAX_BALLS = 5;
        }
        spawnPosition.Set(0, 230, -80);
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
                Instantiate(Balls[index++ % 3], spawnPosition, spawnRotation);
                increaseBalls();
            }
        }
    }
}
