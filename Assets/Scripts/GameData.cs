using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData instance = null;

    public static int result; //0 == null, 1 == lose, 2 == win
    public static int keyNum = 5;
    public static int havingKey = 0;

    public static int portalPosN = 13;     //1~13
    public static int keyPosN = 10;         //1~8
    public static int slenderPosN =  8;    //1~8

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
