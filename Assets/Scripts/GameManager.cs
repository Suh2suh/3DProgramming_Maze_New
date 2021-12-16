using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    List<GameObject> portalPoses;
    public List<GameObject> randPortals;
    public List<Vector3> movePoses;

    List<GameObject> enemyTarget;

    int activePortal = 5;
    int once = 0;

    void Start()
    {
        GameData.result = 0;
        GameData.havingKey = 0;

        portalPoses = new List<GameObject>();
        randPortals = new List<GameObject>();

        movePoses = new List<Vector3>();

        if(GameObject.Find("Slender") != null)
        {
            enemyTarget = GameObject.Find("Slender").GetComponent<EnemyController>().target;
        }

        for (int i = 1; i <= 13; i++)
        {
            portalPoses.Add(GameObject.Find("PortalPos" + i));
        }
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

        if(GameData.result != 0)
        {
            SceneManager.LoadScene("EndScene");
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
}
