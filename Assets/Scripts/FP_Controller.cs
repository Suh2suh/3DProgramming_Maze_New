using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour
{
    float rotateSpd = 4.0f;
    float xRotate = 0f;

    float moveSpd = 4.0f;

    void Start()
    {
        
    }

    void Update()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * rotateSpd;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * rotateSpd;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);     //상하 회전각을 -45~80까지 제한

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        Vector3 move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

        gameObject.transform.position += move * moveSpd * Time.deltaTime;
    }
}
