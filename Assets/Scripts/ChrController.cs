using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrController : MonoBehaviour
{
    float moveSpd = 15.0f;
    float startSpd = 15.0f;
    float rotateSpd = 8.0f;
    float xRotate = 0f;

    Vector3 nowPos;

    bool dashDelay = false;
    float dashTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startSpd = moveSpd;
    }

    // Update is called once per frame
    void Update()
    {
        if(dashDelay)
        {
            dashTime += Time.deltaTime;

            if (dashTime >= 4)
            {
                dashDelay = false;
                dashTime = 0;
            }
        }

        ChrMove();
        ChrRotate();

        ChrDash();
    }

    void ChrMove()
    {
        if (Input.GetKey(KeyCode.W))
            gameObject.transform.Translate(Vector3.forward * moveSpd * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            gameObject.transform.Translate(Vector3.back * moveSpd * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            gameObject.transform.Translate(Vector3.left * moveSpd * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            gameObject.transform.Translate(Vector3.right * moveSpd * Time.deltaTime);
    }
    void ChrRotate()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * rotateSpd;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * rotateSpd;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);     //상하 회전각을 -45~80까지 제한

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

    void ChrDash()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!dashDelay)
            {
                dashTime += Time.deltaTime;

                moveSpd = startSpd * 1.7f;

                if (dashTime >= 2)
                {
                    moveSpd = startSpd;
                    dashDelay = true;
                    dashTime = 0;
                }
            }
        }
        else
        {
            moveSpd = startSpd;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Portal"))
        {
            List<Vector3> movePoses = GameObject.Find("GameManager").GetComponent<GameManager>().movePoses;

            if (movePoses != null)
            {
                gameObject.transform.position = movePoses[Random.Range(0, movePoses.Count)] + (Vector3.forward * 20);
            }
        }

        if(other.name == "Goal")
        {
            if(GameData.havingKey == GameData.keyNum)
                GameData.result = 2;
            else
            {
                if(GameObject.Find("BackPos") != null)
                    transform.position = GameObject.Find("BackPos").transform.position;
                GameObject.Find("UIManager").GetComponent<UIManager>().setNoKeyText();
            }
        }

        if (other.name.Contains("KeyObj"))
        {
            int one = 0;

            if(one == 0)
            {
                if(GameData.havingKey < GameData.keyNum)
                {
                    GameData.havingKey++;
                    Destroy(other.gameObject);

                    GameObject.Find("GameManager").GetComponent<GameManager>().randKeyMaker();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("WALL"))
        {
            nowPos = gameObject.transform.position;
            gameObject.transform.position = nowPos;
        }
    }
}