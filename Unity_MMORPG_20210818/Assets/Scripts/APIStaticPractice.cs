using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIStaticPractice : MonoBehaviour
{
  
    private void Start()
    {
        print("所有攝影機的數量 : " + Camera.allCamerasCount);

        print("2D的重力大小 : " + Physics2D.gravity);

        print("圓周率 : " + Mathf.PI.ToString("0.000"));
        
        Physics2D.gravity = new Vector2(0, -20);
                           
        Time.timeScale = 0.5f;
       
        print("對9.999去小數點 : " + Mathf.FloorToInt(9.999f));

        Vector3 one = new Vector3(1, 1, 1);
        Vector3 two = new Vector3(22, 22, 22);
        print("取得兩點的距離 : " + Vector3.Distance(one, two));

        //開啟連結 
        Application.OpenURL("https://unity.com/");
    }

 
    private  void Update()
    {
        print("是否輸入任意鍵 : " + Input.anyKey);

        print("遊戲經過的時間 : " + Time.timeSinceLevelLoad); //Time.time 所有場景時間都會計算(不會跳場景時間就從新計算)

        print("是否按下空白健 :" + Input.GetKeyDown(KeyCode.Space));
    }
}
