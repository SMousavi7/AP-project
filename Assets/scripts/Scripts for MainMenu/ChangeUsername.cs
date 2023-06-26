using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using TMPro;

public class ChangeUsername : MonoBehaviour
{
    [SerializeField] private GameObject username;
    [SerializeField] private GameObject output;

    public void ChangeName()
    {
        StreamReader sr = new StreamReader("temp_username.txt");
        string name = sr.ReadLine();
        sr.Close();
        string temp1 = username.GetComponent<TextMeshProUGUI>().text;
        File.Delete("temp_username.txt");
        StreamWriter sq = new StreamWriter("temp_username.txt");
        sq.Write(temp1);
        sq.Close();
        StreamReader search = new StreamReader("User.txt");
        bool flag = false;
        while(!search.EndOfStream)
        {
            string[] arr = search.ReadLine().Split(" ");
            if (arr[0].Equals(temp1) )
            {
                flag = true;
                search.Close();
                break;
            }
        }
        if (flag)
        {
            output.GetComponent<TextMeshProUGUI>().text = "this name already exists!!";
            StartCoroutine(delay());
        }
        else
        {
            search.Close();
            search = new StreamReader("User.txt");
            int index = 0;
            int i = 1;
            string newName = "";

            while (!search.EndOfStream)
            {
                string line = search.ReadLine();
                string[] temp = line.Split(" ");
                if (temp[0].Equals(name))
                {
                    index = i;
                    temp[0] = temp1;
                    newName = temp[0] + " " + temp[1] + " " + temp[2] + "\n";
                    search.Close();
                    break;
                }
                else
                {
                    i++;
                }
            }
            search.Close();
            search = new StreamReader("User.txt");
            StreamWriter sw = new StreamWriter("new_User.txt");
            i = 1;
            while (!search.EndOfStream)
            {
                string line = search.ReadLine();
                if (i == index)
                {
                    sw.Write(newName);
                }
                else
                {
                    sw.Write(line);

                }
                sw.Flush();
                i++;
            }
            search.Close();
            sw.Close();
            File.Delete("User.txt");
            File.Move("new_User.txt", "User.txt");
            output.GetComponent<TextMeshProUGUI>().text = "you changed your name successfully";
            StartCoroutine(delay());
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(3);
        output.GetComponent<TextMeshProUGUI>().text = "";
    }
}
