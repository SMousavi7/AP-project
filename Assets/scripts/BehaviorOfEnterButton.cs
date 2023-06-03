using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BehaviorOfEnterButton : MonoBehaviour
{
    string strEmail;
    string strPassword;
    string strUserName;
    public GameObject inputFieldForEmail;
    public GameObject inputFieldForPassword;
    public GameObject inputFieldForUsername;
    public GameObject outputFieldForUsername;
    public void storeName()
    {
        strEmail = inputFieldForEmail.GetComponent<TextMeshProUGUI>().text;
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;
        outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "welcom " + strUserName + "to the game!!!";
       
    }
}
