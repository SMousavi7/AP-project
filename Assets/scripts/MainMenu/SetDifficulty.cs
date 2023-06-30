using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetDifficulty : MonoBehaviour
{
    public void SetHard()
    {
        StreamWriter sw = new StreamWriter("Difficulty.txt");
        sw.Write(3);
        sw.Close();
    }

    public void SetNormal()
    {
        StreamWriter sw = new StreamWriter("Difficulty.txt");
        sw.Write(2);
        sw.Close();
    }

    public void SetEasy()
    {
        StreamWriter sw = new StreamWriter("Difficulty.txt");
        sw.Write(1);
        sw.Close();
    }
}
