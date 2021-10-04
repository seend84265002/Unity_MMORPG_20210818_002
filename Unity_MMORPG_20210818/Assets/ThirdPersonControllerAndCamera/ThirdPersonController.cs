using UnityEngine;          //引用 Unity API  (倉庫，資料與功能)
using UnityEngine.Video;    //引用 影片 API    

namespace Win { 



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
        public bool v3floor;
        public Vector3 v3checkGroundoffect;
        [Range(0, 3)]
        public float v3radius = 0.2f;

        [Header("音效檔案")]
        public AudioClip Audiojump;
        public AudioClip Audiofloor;

        [Header("動畫參數")]
        public string aniWalk = "走路開關";
        public string aniRun = "跑步開關";
        public string aniHurt = "受傷觸發";
        public string aniDie = "死亡觸發";
        public string aniJump = "跳耀觸發";
        public string aniIsFloor = "是否在地板上";

        public GameObject playerObject;

        private ThridPersonCamera thridPersonCamera;
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;

        [Header("面向速度"), Range(0, 50)]
        public float speedLookAt = 2;




        #endregion

        #region 屬性 Property 
        /// <summary>
        /// 跳耀
        /// </summary>
        /// <paran mane="keyJump">跳耀按鍵</paran>
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        /// <summary>
        /// 隨機音效音量
        /// </summary>
        /// <paran name="volumeRandom">音效音量</paran>
        private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
    #endregion

    #region 方法 Method
    //定義與實作 較複雜程式的區塊，功能
    //方法語法 : 修飾詞 傳回資料類型 方法名稱 (參數1,...參數N){ 程式區塊 }
    //常用傳回類型 : 無傳回 void - 此方法沒有傳回資料
    //格式化 排版 ctrl + K + D
    //摺疊 ctrl + M O
    //展開 ctrl + M L
    //名稱顏色為淡黃色 - 沒有被呼叫
    //名稱顏色為黃色 - 有被呼叫

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="speedmove">移動速度</param>    
    private void Move(float speedmove)
        {
            //請取消 Animator 屬性 Apply Root Motion :勾選時使用動畫位移資訊
            //鋼體.加速度 = 三維向量，加速度用來控制鋼體 三個軸向的運動速度
            //前方*輸入值 =移動速度
            //使用前後左右軸向運動並保持原本的地心引力
            rig.velocity = transform.forward * MoveInput("Vertical") * speedmove +
                           transform.right * MoveInput("Horizontal") * speedmove +
                           Vector3.up * rig.velocity.y;


        }
        /// <summary>
        /// 移動按鍵輸入
        /// </summary>
        /// <param name="axisName">要取的軸向名稱</param>
        /// <returns>回傳值浮點數0</returns>
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }
        /// <summary>
        /// 檢查地板
        /// </summary>
        /// <returns>回傳布林值false</returns>
        private bool CheckGround()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position +
                transform.right * v3checkGroundoffect.x +
                transform.up * v3checkGroundoffect.y +
                transform.forward * v3checkGroundoffect.z
                , v3radius, 1 << 3); //因為是3所以寫 1<<3   以此類推  10就是 1<<10
                                     //print("球體碰到第一個物件 : " + hits[0].name);
                                     // 傳回 碰撞陣列數量 > 0 ，只要碰到指定圖層，物件就代表在地面上 
            if (!v3floor && hits.Length > 0) aud.PlayOneShot(Audiofloor, volumeRandom);
            v3floor = hits.Length > 0;
            return hits.Length > 0;
        }
        /// <summary>
        /// 跳躍
        /// </summary>
        private void Jump()
        {
            //print("球體碰到第一個物件 : " + CheckGround());
            //並且 &&
            //如果 在地面上 並且按下空白鍵 就跳耀
            //if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
            if (CheckGround() && keyJump)
            {
                //print("跳起來");
                rig.AddForce(transform.up * jump);

                aud.PlayOneShot( Audiojump, volumeRandom);
            }
        }
        /// <summary>
        /// 更新動畫
        /// </summary>
        private void UpdateAnimation()
        {
            ani.SetBool(aniWalk, MoveInput("Vertical") != 0 ||
                                 MoveInput("Horizontal") != 0);
            ani.SetBool(aniIsFloor, v3floor);
            if (keyJump) ani.SetTrigger(aniJump);
            //ani.SetBool(aniWalk, true);
        }


        private void LookAtForward()
        {
            if (Mathf.Abs(MoveInput("Vertical")) > 0.1f)
            {//取得前方角度 = 四元 .面相角度(前方座標-本身座標)
                Quaternion angle = Quaternion.LookRotation(thridPersonCamera.posForawrd - transform.position);
                //此物件的角度 = 四元 .差件
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime*speedLookAt);

            }
        }
    
      
        #endregion


        #region 事件 Event 
        // 特定時間點會執行的方法，程式的入口 Start 等於 Console Main
        // 開始事件:遊戲開始執行一次，處理初始化，取的資料..等等
        private void Start()
        {
            //1.物件欄位名稱，取得元件(類型(元件類型)) 當作 元件類型;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            //2.此腳本遊戲物件，取得原件<泛型>();
            rig = gameObject.GetComponent<Rigidbody>();
            //3.取得元件<泛型>();
            //類別可以使用繼承類別(父親別)的成員，公開或保護
            ani = GetComponent<Animator>();
            //攝影機類別= 透過類型尋找物件<泛型>();
            //FindObjectOfType 不要放在 Updata 內使用會造成大量效能負擔
            thridPersonCamera = FindObjectOfType<ThridPersonCamera>();
        }



        //更新事件:一秒約執行 60 次 ，60FPS -Frame Per Secound ，用來處理慣性運動，移動物件，監聽玩家輸入按鍵。
        private void Update()
        {   

            Jump();
            UpdateAnimation();
            LookAtForward();
            //Move(speed);
        }



        // 固定更新事件 : 固定 0.02 秒執行一次
        //用來處理物理行為: 例如 Rigidbody API
        private void FixedUpdate()
        {
            Move(speed);
        }
        private void OnDrawGizmos()
        {
            //1.指定 顏色
            //2.繪製圖形

            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            // transform 與此腳本在同階層的 transform 元件
            Gizmos.DrawSphere(transform.position +
                transform.right * v3checkGroundoffect.x +
                transform.up * v3checkGroundoffect.y +
                transform.forward * v3checkGroundoffect.z
                , v3radius);   //Gizmos.DrawSphere(座標 x y z ，半徑)
        }
        #endregion



    }
}
