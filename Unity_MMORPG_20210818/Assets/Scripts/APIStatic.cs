using UnityEngine;
/// <summary>
/// 認識API靜態 Static
/// </summary>
public class APIStatic : MonoBehaviour
{
    private void Start()
    {
        #region   靜態屬性
        //取得     Get Set       
        //語法
        //類別名稱.靜態屬性
        //獲得寶物的機率 
        float a = Random.value;
        print("取的靜態屬性，隨機值 : " + a);

        //取得    Set       
        //語法
        //類別名稱.靜態屬性 指定 值;
        //只要看到 Read Only 就是不能設定 (唯讀不能設定)
        //Cursor.visible = false;

        #endregion


        #region   靜態方法
        //呼叫: 參數 傳回           
        //簽章: 參數 傳回
        //語法 
        //類別名稱.靜態方法(對應引數)
        float range = Random.Range (10.5f, 20.9f);
        print("隨機範圍  10.5 - 20.9 : " + range);
        //API 說明很重要 使用整數是不包含最大值
        int rangeInt = Random.Range(1, 3);
        print("隨機範圍 1-3 (不包含3) : " + rangeInt);
        #endregion
    }
    private void Update()
    {
        #region  靜態屬性
        //時間第一偵0.02秒 ，用來計算(場景)過關的時間 ，紀錄場景的時間
        //print("經過多久:" + Time.timeSinceLevelLoad);
        #endregion

        #region  靜態方法
        float h = Input.GetAxis("Horizontal");
        print("水平值 : " + h);
        #endregion
    }

}
