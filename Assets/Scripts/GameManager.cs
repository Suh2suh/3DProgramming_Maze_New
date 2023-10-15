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

    List<List<GameObject>> SlenderTargets = new List<List<GameObject>>();

    [SerializeField] AudioSource bellSound;

    int activePortal = 10;

    List<int> recentKeyPoses = new List<int>();
    public GameObject KeyObject;

    [SerializeField] List<GameObject> Slenders;

    void Start()
    {
        GameData.result = 0;
        GameData.havingKey = 0;

        bellSound.volume = 0f;

        portalPoses = new List<GameObject>();
        randPortals = new List<GameObject>();
        keyPoses = new List<GameObject>();

        movePoses = new List<Vector3>();

        //ù��° ������ ����Ʈ �߰�
        SlenderTargets.Add(Slenders[0].GetComponent<EnemyController>().target);

        for (int i = 1; i <= GameData.portalPosN; i++)
        {
            portalPoses.Add(GameObject.Find("PortalPos" + i));
        }

        for(int i = 1; i <= GameData.keyPosN; i++)
        {
            keyPoses.Add(GameObject.Find("KeyPos" + i));
        }

        randKeyMaker(); //������ �� ���� �ϳ� ����
    }


    bool IsPlayerFound = false;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        for (int i = 0; i < SlenderTargets.Count; i++)
        {
            if (SlenderTargets[i].Count > 0 && IsPlayerFound)
            {
                randPortalMaker();

                bellSound.volume = 0.3f;

                IsPlayerFound = false;
            }
            else if (SlenderTargets[i].Count == 0 && !IsPlayerFound)
            {
                if (!IsPlayerFound)
                {
                    bellSound.volume = 0f;

                    IsPlayerFound = true;
                }
                randPortalDelete();
            }
        }


        if(GameData.result == 1)                                        //�й��ϸ� endscene����
        {
            SceneManager.LoadScene("EndScene");
        }
        else if(GameData.result == 2)
		{
            SceneManager.LoadScene("ClearScene");
        }

        if (GameData.havingKey == GameData.keyNum)  //���� �� ã������ run text ����
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().onRunText();

            for (int i = 0; i < Slenders.Count; i++)
            {
                if (SlenderTargets[i].Count == 0)
                {
                    SlenderTargets[i].Add(GameObject.Find("Player"));
                }
            }
        }
    }

    public void AddSlender()
	{
        switch(GameData.havingKey)
		{
            case 1:
                Slenders[1].SetActive(true);
                SlenderTargets.Add(Slenders[1].GetComponent<EnemyController>().target);
                break;
            case 2:
                Slenders[2].SetActive(true);
                SlenderTargets.Add(Slenders[2].GetComponent<EnemyController>().target);
                break;
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

        for(int i = 0; i<GameData.keyNum; i++)
		{
            if (GameData.havingKey < GameData.keyNum)  //���� ��ġ�� ���� ���� (���� �� �� ã���� ������)
            {
                do
                {
                    nowKeyPos = Random.Range(0, GameData.keyPosN);
                } while (recentKeyPoses.Contains(nowKeyPos));              //���� �ֱ��� ��ġ���� �������� ���� (�÷��̾ ������ ���踦 ���� ��)

                Instantiate(KeyObject, keyPoses[nowKeyPos].transform);

                recentKeyPoses.Add(nowKeyPos);

                Debug.Log("Made a Key in " + nowKeyPos);
            }
        }
    }
}
