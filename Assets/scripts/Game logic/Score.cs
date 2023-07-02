using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] GameObject player;
    private static int score = 0;
    private static int mult = 1;
    private static int difficulty = 1;
    public static void setMult(int mult)
    {
        difficulty = PlayerMovement.difficultyLevel;
        Score.mult = mult;
    }
    public static int getScore()
    {
        return score;
    }
    public static void addScore(int score)
    {
        Score.score += score * mult;
    }

    public void endgameReset()
    {
        BallMovement.resetDMG();
        score = 0;
    }

    public void reset()
    {

        PlayerMovement p = player.GetComponent<PlayerMovement>();
        p.sendRecord();
        p.resetClock();
        BallMovement.resetDMG();
        score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //read difficulty from filepublic void readDifficulty()
        StreamReader sr = new StreamReader("Difficulty.txt");
        string str = sr.ReadLine();
        sr.Close();
        difficulty = int.Parse(str);
        scoreText.text = score.ToString() + " Points";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + " Points";
    }
}
