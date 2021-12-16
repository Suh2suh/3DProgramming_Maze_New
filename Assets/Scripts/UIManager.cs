using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    List<GameObject> RunTexts;

    GameObject KeyNum;
    GameObject KeyNumL;

    float runOffSec;
    float runOnSec;

    private void Start()
    {
        KeyNum = GameObject.Find("KeyNum");
        KeyNumL = GameObject.Find("KeyNumLimit");


        RunTexts = new List<GameObject>();
        GameObject RunTextsP = GameObject.Find("Texts").transform.Find("RunTexts").gameObject;
        for (int i = 1; i <= 12; i++)
        {
            RunTexts.Add(RunTextsP.transform.Find("RunText" + i).gameObject);
        }
    }

    private void Update()
    {
        KeyNumL.GetComponent<Text>().text = GameData.keyNum.ToString();
        KeyNum.GetComponent<Text>().text = GameData.havingKey.ToString();


        if(RunTexts[11].activeSelf == true)
        {
            runOffSec += Time.deltaTime;

            if(runOffSec >= 1)
            {
                for (int i = 0; i < 12; i++)
                {
                    RunTexts[i].SetActive(false);
                }
                runOffSec = 0;
            }
        }

        if(GameData.havingKey == GameData.keyNum)
        {
            if(GameObject.Find("KeyText").activeSelf != false)
                GameObject.Find("KeyText").SetActive(false);
        }
    }

    public void setNoKeyText()
    {
        GameObject noKeyPanel = GameObject.Find("Texts").transform.Find("NoKeyPanel").gameObject;

        if (noKeyPanel.activeSelf != true)
            noKeyPanel.SetActive(true);

        Text noKeyText = noKeyPanel.transform.Find("NoKeyText").gameObject.GetComponent<Text>();
        
        if(GameData.havingKey == 0)
        {
            noKeyText.text = "I DO NOT HAVE EVEN A SINGLE KEY...";
        }
        else if(GameData.havingKey < GameData.keyNum)
        {
            noKeyText.text = "IT SEEMS I NEED MORE KEYS\nTO GET OUT OF HERE";
        }
    }

    public void onRunText()
    {
        if ((RunTexts[11].activeSelf != true))
        {
            runOnSec += Time.deltaTime;

            if (runOnSec >= 2.4)
                RunTexts[11].SetActive(true);
            else if (runOnSec >= 2.2)
                RunTexts[10].SetActive(true);
            else if (runOnSec >= 2)
                RunTexts[9].SetActive(true);
            else if (runOnSec >= 1.8)
                RunTexts[8].SetActive(true);
            else if (runOnSec >= 1.6)
                RunTexts[7].SetActive(true);
            else if (runOnSec >= 1.4)
                RunTexts[6].SetActive(true);
            else if (runOnSec >= 1.2)
                RunTexts[5].SetActive(true);
            else if (runOnSec >= 1)
                RunTexts[4].SetActive(true);
            else if (runOnSec >= 0.8)
                RunTexts[3].SetActive(true);
            else if (runOnSec >= 0.6)
                RunTexts[2].SetActive(true);
            else if (runOnSec >= 0.4)
                RunTexts[1].SetActive(true);
            else if (runOnSec >= 0.2)
                RunTexts[0].SetActive(true);

        }
    }

    /*
    [System.Obsolete]
    public static void delNoKeyText()
    {
        GameObject noKeyPanel = GameObject.Find("Texts").transform.FindChild("NoKeyPanel").gameObject;

        if(noKeyPanel.activeSelf != false)
        {
            noKeyPanel.SetActive(false);
        }
    }
    */
}
