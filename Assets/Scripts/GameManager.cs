using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    List<GameObject> portalPoses;
    public List<GameObject> randPortals;
    public List<Vector3> movePoses;

    List<GameObject> keyPoses;

    List<GameObject> enemyTarget;

    int activePortal = 5;
    int once = 0;

    int recentKeyPos = -1;
    public GameObject KeyObject;

    void Start()
    {
        GameData.result = 0;
        GameData.havingKey = 0;

        portalPoses = new List<GameObject>();
        randPortals = new List<GameObject>();
        keyPoses = new List<GameObject>();

        movePoses = new List<Vector3>();

        if(GameObject.Find("Slender") != null)
        {
            enemyTarget = GameObject.Find("Slender").GetComponent<EnemyController>().target;
        }

        for (int i = 1; i <= GameData.portalPosN; i++)
        {
            portalPoses.Add(GameObject.Find("PortalPos" + i));
        }

        for(int i = 1; i <= GameData.keyPosN; i++)
        {
            keyPoses.Add(GameObject.Find("KeyPos" + i));
        }

        randKeyMaker(); //시작할 때 열쇠 하나 생성
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(enemyTarget.Count > 0 && once == 0)
        {
            randPortalMaker();

            once = 1;
        }
        else if(enemyTarget.Count == 0 && once == 1)
        {
            if(once != 0)
            {
                once = 0;
            }
            randPortalDelete();
        }

        if(GameData.result != 0)                                        //승리나 패배하면 endscene으로
        {
            SceneManager.LoadScene("EndScene");
        }

        if(GameData.havingKey == GameData.keyNum)  //열쇠 다 찾았으면 run text 띄우기
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().onRunText();
            if(enemyTarget.Count == 0)
            {
                enemyTarget.Add(GameObject.Find("Player"));
            }
        }
    }

    [System.Obsolete]
    void randPortalMaker()
    {
        do
        {
            int portalNum = Random.Range(0, portalPoses.Count);
            if (!randPortals.Contains(portalPoses[portalNum]))
            {
                randPortals.Add(portalPoses[portalNum]);
            }
        } while (randPortals.Count < activePortal);

        Debug.Log(randPortals.Count);

        for (int i = 0; i < randPortals.Count; i++)
        {
            Debug.Log(randPortals[i]);
            GameObject nowPortal = randPortals[i].transform.FindChild("Portal").gameObject;

            movePoses.Add(randPortals[i].transform.position);

            if (nowPortal != null)
            {
                if (nowPortal.activeSelf != true)
                {
                    nowPortal.SetActive(true);
                }
            }
        }
    }

    [System.Obsolete]
    void randPortalDelete()
    {
        for (int i = 0; i < randPortals.Count; i++)
        {
            GameObject nowPortal = randPortals[i].transform.FindChild("Portal").gameObject;

            if (nowPortal != null)
            {
                if (nowPortal.activeSelf != false)
                {
                    nowPortal.SetActive(false);
                }
            }
        }

        randPortals.Clear();
        movePoses.Clear();
    }

    public void randKeyMaker()
    {
        int nowKeyPos;
        if (GameData.havingKey < GameData.keyNum)  //랜덤 위치에 열쇠 생성 (열쇠 다 못 찾았을 때에만)
        {
            do
            {
                nowKeyPos = Random.Range(0, GameData.keyPosN);
            } while (nowKeyPos == recentKeyPos);              //가장 최근의 위치에는 생성하지 않음 (플레이어가 직전에 열쇠를 얻은 곳)

            Instantiate(KeyObject, keyPoses[nowKeyPos].transform);

            recentKeyPos = nowKeyPos;

            Debug.Log("Made a Key in " + nowKeyPos);
        }
    }
}
