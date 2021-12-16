using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrController : MonoBehaviour
{
    float moveSpd = 15.0f;
    float startSpd = 10.0f;
    float rotateSpd = 8.0f;
    float xRotate = 0f;

    //float dashDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        startSpd = moveSpd;
    }

    // Update is called once per frame
    void Update()
    {
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
            moveSpd = startSpd * 2f;
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
            GameData.result = 2;
        }
    }
}