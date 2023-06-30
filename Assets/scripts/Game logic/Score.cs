using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    private static int score = 0;
    private static int highScore = 0;
    public static int getScore()
    {
        return score;
    }
    public static void addScore(int score)
    {
        Score.score += score;
    }
 
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + " Points";
        highScoreText.text = highScore.ToString() + " Points";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + " Points";
        highScoreText.text = highScore.ToString() + " Points";
    }
}
