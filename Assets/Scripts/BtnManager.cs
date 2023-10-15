using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{

    private void Start()
    {
        if(GameObject.Find("Result_Text") != null)
        {
            if(GameData.result == 1)
            {
                GameObject.Find("Result_Text").GetComponent<Text>().text = "Couldn't escape from the curse...";
            }
            else if(GameData.result == 2)
            {
                GameObject.Find("Result_Text").GetComponent<Text>().text = "tere is no next chance...";
            }
        }
    }
    public static void startClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    public static void quitClick()
    {
        //알림창 띄우기
        Application.Quit();
    }
}
