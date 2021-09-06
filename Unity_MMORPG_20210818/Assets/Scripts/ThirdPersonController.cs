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
    public bool floor ;
    public Vector3  v3checkGroundoffect  ;
    [Range(0, 3)]
    public float radius = 0.2f ;

    [Header("音效檔案")]
    public AudioClip Audiojump;
    public AudioClip Audiofloor;

    [Header("動畫參數")]
    public string aniWalk = "走路開關" ;
    public string aniRun  = "跑步開關" ;
    public string aniHurt = "受傷觸發" ;
    public string aniDie  = "死亡觸發" ;

    public AudioSource aud;
    public Rigidbody rdbody;
    public Animator ani;


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

    #endregion
    #region 方法 Method

    #endregion
    #region 事件 Event 

    #endregion



}
