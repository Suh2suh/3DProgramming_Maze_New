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
    bool posFix = false;

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

                GameObject.Find("UIManager").GetComponent<UIManager>().OnDashText();
                dashTime = 0;
                //대쉬가능 텍스트 띄우기
            }
        }

        ChrMove();
        ChrRotate();

        ChrDash();

        if (posFix == true)
        {
            gameObject.transform.position = nowPos;
        }
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
            if(!IsTouchingWall)
			{
                if (!dashDelay)
                {

                    dashTime += Time.deltaTime;

                    moveSpd = startSpd * 2f;

                    if (dashTime >= 5)
                    {
                        moveSpd = startSpd;
                        dashDelay = true;
                        dashTime = 0;

                        //대쉬가능 텍스트 없애기(한 번만)
                        GameObject.Find("UIManager").GetComponent<UIManager>().RemoveDashText();
                    }
                }
            }
        }
        else
        {
            moveSpd = startSpd;
        }
    }


    bool IsTouchingWall = false;

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
        
        if (other.transform.tag == "Key")
        {
            int one = 0;

            if(one == 0)
            {
                if(GameData.havingKey < GameData.keyNum)
                {
                    GameData.havingKey++;

                    GameObject.Find("GameManager").GetComponent<GameManager>().AddSlender();

                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name.Contains("Wall")){
            moveSpd = 0;
            IsTouchingWall = true;
        }
    }

	private void OnCollisionExit(Collision collision)
	{
        if (collision.gameObject.name.Contains("Wall"))
        {
            IsTouchingWall = false;
        }
    }
}