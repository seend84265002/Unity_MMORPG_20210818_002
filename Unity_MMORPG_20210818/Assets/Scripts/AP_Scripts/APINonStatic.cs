using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 認識API 非靜態 Non static
/// </summary>
public class APINonStatic : MonoBehaviour
{
    public Transform tra1;          //修飾詞 要存取非靜態的類別 欄位名稱;  物件就是Camera ...等 Unity實體物件。
    public Camera cam;
    public Light lig;
    private void Start()
    {
        #region 非靜態屬性
        //動態與靜態不一樣
        //1.需要實體物件
        //2.取的實體物件，定義欄位並將要的物件存入欄位
        //3.遊戲物件，元件必須存在的場景內
        //取得 Get
        //語法:欄位名稱，非靜態屬性
        print("攝影機的座標 : " + tra1.position);
        print("攝影機的深度 : " + cam.depth);

        //設定 Set
        //語法 : 欄位名稱 .非靜態屬性 指定值:

        tra1.position = new Vector3(55, 66, 77);
        cam.depth = 7;
        print("攝影機的座標 : " + tra1.position);
        print("攝影機的深度 : " + cam.depth);
        #endregion

        #region 非靜態方法
        //呼叫 
        //語法
        //欄位名稱，非靜態方法名稱(對應參數):
        lig.Reset();



        #endregion
    }
}
