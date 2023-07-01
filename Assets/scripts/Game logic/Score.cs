using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private static int score = 0;
    private static int mult = 1;
    private static int difficulty = 1;
    public static void setMult(int mult)
    {
        Score.mult = mult * difficulty;
    }
    public static int getScore()
    {
        return score;
    }
    public static void addScore(int score)
    {
        Score.score += score * mult;
    }

    public void reset()
    {
        PlayerMovement p = new PlayerMovement();
        p.sendRecord();
        score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //read difficulty from file
        difficulty = PlayerMovement.difficultyLevel;
        scoreText.text = score.ToString() + " Points";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + " Points";
    }
}
