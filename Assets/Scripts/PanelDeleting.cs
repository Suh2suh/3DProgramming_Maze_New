using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDeleting : MonoBehaviour
{
    float time;

    private void Update()
    {
        time += Time.deltaTime;

        if(time >= 3)
        {
            if (gameObject.activeSelf != false)
            {
                time = 0f;
                gameObject.SetActive(false);
            }
        }
    }
}
