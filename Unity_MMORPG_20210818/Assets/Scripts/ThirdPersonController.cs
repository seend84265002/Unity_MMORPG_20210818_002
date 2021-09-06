using UnityEngine;          //引用 Unity API  (倉庫，資料與功能)
using UnityEngine.Video;    //引用 影片 API    

//修飾詞 類別 類別名稱 : 繼承類別 
// MonoBehaviour Unity 基底類別 :要掛在物件上一定要繼承
// 繼承後會享有該類別的成員
// 在類別以及成員上方添加三條斜線會添加摘要
// 常用成員:欄位 Frid 、屬性 Property(變數)、 方法 Method 、 事件 Event 
/// <summary>
/// Wen 2021.09.06
/// 第三人稱控制器
/// 移動，跳躍 
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region 欄位 Freld
    /*
    //儲存遊戲資料，例如:移動速度，跳躍高度..
    //常用四大類型:整數，浮點數，字串，布林值
    //欄位語法:修飾詞，資料類型，欄位名稱(指定 預設值) 結尾
    //修飾詞:
    //1.公開 public  允許其他類別存取，顯示在屬性面板，需要調整的資料設定為公開
    //2.私人 private  禁止其他類別存取，隱藏在屬性面板， 預設值
    //   Unity 以屬性面板為主
    //   恢復程式預設值請按....>Reset
    //  欄位屬性 Attribute :輔助欄位資料
    //  欄位屬性語法 : [屬性名稱(屬性值)]
    //  Header 標題
    //  Tooltip 提示:滑鼠停留在欄位名稱上會顯示的視窗
    //  Range 範圍:可以使用在數值類型資料上，例如: int , float
    [Header("移動速度"), Tooltip("用來調整角色移動速度"), Range(0, 500)]
    public float speed = 10.5f;
    [Header("跳躍高度"), Tooltip("用來調整角色跳躍高度"), Range(0, 1000)]
    public int jump = 100;

    [Header("檢查地板資料")]
    [Tooltip("檢查角色是否在地板上面")]
    public bool floor;
    public Vector3 v3checkGroundoffect;
    [Range(0, 3)]
    public float radius = 0.2f;

    [Header("音效檔案")]
    public AudioClip Audiojump;
    public AudioClip Audiofloor;

    [Header("動畫參數")]
    public string aniWalk = "走路開關";
    public string aniRun = "跑步開關";
    public string aniHurt = "受傷觸發";
    public string aniDie = "死亡觸發";

    public AudioSource aud;
    public Rigidbody rdbody;
    public Animator ani;
    */



    #region Unity 資料類型
    /** 練習 Unity  資料類型       UML  應用程式 開發專案 使用
    //顏色 color
    public Color color;
    public Color white = Color.white;                       //內建顏色
    public Color yellow = Color.yellow;
    public Color color1 = new Color(0.5f, 0.5f, 0);         //自訂顏色 RGB
    public Color color2 = new Color(0, 0.5f, 0.5f, 0.5f);   //自訂顏色 RGBA   
    //座標 Vector2-4
    public Vector2 v2;                                      // Vector2 2維空間函數   (x,y)
    public Vector2 v2Right = Vector2.right;                 //往右邊
    public Vector2 v2UP = Vector2.up;                       //往前走
    public Vector2 v2One = Vector2.one;                         
    public Vector2 v2Custom = new Vector2(7.5f, 100.9f);
    public Vector3 v3 = new Vector3(1, 2, 3);               // Vector3  3維空間函數  (x,y,z)
    public Vector3 v3Forward = Vector3.forward;
    public Vector4 v4 = new Vector4(2, 1, 3, 5);            // Vector4  4維空間函數  (x,y,z,角度)
    //按鍵 列舉資料 enum
    public KeyCode key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    //遊戲資料類型: 不能指定預設值
    // 存放 Project 專案內的資料
    public AudioClip sound;              // 音效 mp3 ,ogg ,wav
    public VideoClip videoClip;          // 影片 mp4
    public Sprite sprite;                // 圖片 png ,jpge 不支援 gif  
    public Texture2D texture2d;          // 2D 圖片 png ,jpge 
    public Material material;            // 材質球   
    [Header("元件")]
    //元件 Component :屬性面上可以折疊的
    public Transform tra;
    public Animation aniold;
    public Animator aniNew;
    public Light lig;
    public Camera cam;

    //綠色蚯蚓
    //1.建議不要使用的名稱
    //2.使用過時的API
    /**/

    #endregion
    #endregion

    #region 屬性 Property 

    #region 屬性練習
    
     
    //儲存資料，與欄位相同
    //差異在於:可以設定存取權限
    //屬性語法:修飾詞 資料類型 屬性名稱{ 取; 存; }
    /*
    public int readAndWrite { get; set; }
    public int read { get; }
    // 唯讀屬性:透過get 設定預設值，關鍵字 return 為傳回值
    public int readValue {
        get
        {
            return 77;
        }
    }
    //唯寫屬性是禁止的
    //public int Write {  set; }
    // value 指的是指定的值
    private int _hp;
    public int hp {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    */

    public KeyCode keyJump;



    #endregion
    #endregion

    #region 方法 Method
    //定義與實作 較複雜程式的區塊，功能
    //方法語法 : 修飾詞 傳回資料類型 方法名稱 (參數1,...參數N){ 程式區塊 }
    //常用傳回類型 : 無傳回 void - 此方法沒有傳回資料
    //格式化 排版 ctrl + K + D
    //名稱顏色為淡黃色 - 沒有被呼叫
    //名稱顏色為黃色 - 有被呼叫

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="speed">移動速度</param>    
    private void move(float speed)
    {
        
    }
    /// <summary>
    /// 移動按鍵輸入
    /// </summary>
    /// <returns>回傳值浮點數0</returns>
    private float move()
    {
        return 0;
    }
    /// <summary>
    /// 檢查地板
    /// </summary>
    /// <returns>回傳布林值false</returns>
    private bool Floor()
    {
        return false;
    }
    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
      
    }
    /// <summary>
    /// 更新動畫
    /// </summary>
    private void updata()
    {

    }

    /*
    private void Text()
    {
        print("我是自訂的方法~~");
    }
    private int RetureJump()
    {
        return 999;
    }
    */
    #region 參數語法
    /*
    //參數語法 : 資料類型 參數名稱
    private void Skill(int damage,string effect = "灰塵特效",string sound ="喀喀喀")
    {
        print("參數版本-傷害值:"  + damage);
        print("參數版本-技能特效:" + effect);
        print("參數版本-音效:" + sound);
    }
    
    /// <summary>
    /// 計算BMI的方法 BMI=weight/(height*height)
    /// </summary>
    /// <param name="weight">體重，單位公斤</param>
    /// <param name="height">身高，單位公尺</param>
    /// <param name="name">姓名</param>
    /// <returns></returns>
    private float BMI(float weight,float height,string name)
    {
        print(name + "的BMI值:");
        return weight / (height * height);
    }
    
    //不使用參數，降低維護與擴充性
    private void skill100()
    {
        print("參數版本-傷害值" + 100);
        print("技能特效");
    }
    private void skill200()
    {
        print("參數版本-傷害值" + 200);
        print("技能特效");
    }
    private void skill1000()
    {
        print("參數版本-傷害值" + 1000);
        print("技能特效");
    }
    */
    #endregion
    #endregion

    #region 事件 Event 
    // 特定時間點會執行的方法，程式的入口 Start 等於 Console Main
    // 開始事件:遊戲開始執行一次，處理初始化，取的資料..等等
    private void Start()
    {
        #region  輸出方法
        /*print("Hello Wolrd");

        Debug.Log("一般訊息");
        Debug.LogError("錯誤訊息");
        Debug.LogWarning("警告訊息");*/
        #endregion
        #region   屬性練習
        /*
        print("欄位資料 - 移動速度:" + speed);
        print("屬性資料 - 讀寫資料:" + readAndWrite);
        speed = 10f + speed;
        readAndWrite = 90;
        print("修改後的資料");
        print("欄位資料 - 移動速度:" + speed);
        print("屬性資料 - 讀寫資料:" + readAndWrite);

        //read = -7;  //唯獨屬性不能設定 set
        print("唯讀屬性:" + read);
        print("唯讀屬性:" + readAndWrite);

        //屬性存取練習
        print("前HP:" + hp);
        hp = 100;
        print("後HP:" + hp);

        print(BMI(73f, 1.7f, "你是誰??"));
        
        // 呼叫自訂的方法:方法名稱();
        Text();
        //呼叫有傳回值的方法
        //1.區域變數指的是指定傳回值-區域變數僅能在此結構(大括號)內存取
        int j = RetureJump();
        print("跳躍值:" + j);
        //2.將傳回方法當成值使用
        print("A跳躍值:" + (RetureJump() + 1));

        Skill(300);
        Skill(500,sound:"咻咻咻");         //有多個參數可使用指名參數的語法  參數語法:值  
        */
        #endregion

        move(100f);
        move();
        Floor();
        Jump();
        Update();


    }


    //更新事件:一秒約執行 60 次 ，60FPS -Frame Per Secound ，用來處理慣性運動，移動物件，監聽玩家輸入按鍵。
    private void Update()
    {
        //print("YO YO YO~");
    }


    #endregion



}
