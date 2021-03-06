using System.Collections; //呼叫協成程序
using UnityEngine;
using UnityEngine.AI;
using Wen.Dialogue;
namespace Wen.Enemy
{
    /// <summary>
    /// 敵人行為
    /// 敵人狀態 : 等待 走路 追蹤 攻擊 受傷 死亡
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region 欄位 公開
        [Header("移動速度"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("攻擊力"), Range(0, 200)]
        public float attack = 35;
        [Header("範圍:追蹤與攻擊")]
        [Range(0,7)]
        public float rangeAttack = 5;
        [Range(7, 20)]
        public float rangeTrack = 15;
        [Header("等待隨機秒數")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("走路隨機秒數")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);
        [Header("攻擊區域 位移 與尺寸")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        [Header("攻擊冷卻時間"), Range(0, 5)]
        public float timeAttack=2.5f;
        [Header("攻擊延遲傳送傷害時間"), Range(0, 5)]
        public float delaySendDamage = 0.5f;
        [Header("面向玩家的速度"), Range(0, 50)]
        public float speendLookAt = 10;
        [Header("NPC 名稱")]
        public string nameNPC = "小朋友";



        #endregion

        #region 欄位 私人
        [SerializeField]        //序列化欄位  SerializeField可以顯示私人欄位
        private StateEmeny state;
        private NavMeshAgent nma;
        private Animator ani;
        private string parameterIdleWalk ="走路開關";
        /// <summary>
        /// 隨機行走座標
        /// </summary>
        private Vector3 v3RandomWalk
        {
            get => Random.insideUnitSphere * rangeTrack + transform.position;
        }
        private Vector3 v3RandomWalkFinal;   //最終座標(運算完後的)
        /// <summary>
        /// 檢查玩家是否在追蹤範圍內
        /// </summary>
        private bool PlayerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }
        private Transform traplayer;
        private string nameplayer = "女主角";
        
        private bool isIdle; //是否等待狀態
        private bool isWalk; //是否走路狀態
        private bool isTrack; //使否追蹤狀態
        private string paramterAttack="攻擊觸發";
        private bool isAttack;
        private bool targetIsDead;
        private NPC npc;
        private HurtSystem hurtSystem;

        #endregion


        #region 繪製圖˙型
        private void OnDrawGizmos()
        {
            #region 攻擊範圍 追蹤範圍 與隨機行走座標
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);
            if (state == StateEmeny.Walk)
            {
                Gizmos.color = new Color(1f, 0, 0.2f, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }
            #endregion

            #region 攻擊碰撞判定區域
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);

            //繪製方形，需要跟者角色旋轉時請使用 matrix 指定座標角度與尺寸
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position+
                transform.right*v3AttackOffset.x+
                transform.up*v3AttackOffset.y+
                transform.forward*v3AttackOffset.z, 
                transform.rotation,
                transform.localScale);    //取的角度

            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion




        }
        #endregion
        /// <summary>
        /// 事件
        /// </summary>
        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = speed;
            hurtSystem = GetComponent<HurtSystem>();

            traplayer = GameObject.Find(nameplayer).transform;
            npc = GameObject.Find(nameNPC).GetComponent<NPC>();
            //受傷系統 - 死亡事件 觸發時 請 NPC 更新數量
            //AddListener(方法) 添加 監聽器 (方法)
            hurtSystem.onDead.AddListener(npc.UpdataMissionCount);
            nma.SetDestination(transform.position);             //導覽器 一開始就先動作
            
        }

        private void Update()
        {
            StartManager();
        }

        /// <summary>
        /// 狀態管理
        /// </summary>
        private void StartManager()
        {
            switch (state)
            {
                case StateEmeny.Idle:
                    Idle();
                    break;
                case StateEmeny.Walk:
                    Walk();
                    break;
                case StateEmeny.Track:
                    Track();
                    break;
                case StateEmeny.Attack:
                    Attack();
                    break;
                case StateEmeny.Hurt:
                    break;
                case StateEmeny.Dead:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 等待 隨機秒數走路狀態
        /// </summary>
        private void Idle()
        {
            if (!targetIsDead && PlayerInTrackRange) state = StateEmeny.Track;       //如果玩家進入追蹤圍內 切換追蹤狀態
            #region  進入狀態
            if (isIdle) return;
            isIdle = true;
            #endregion

            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(IdleEffect());
        }
        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);
            state = StateEmeny.Walk;   //走路狀態

            #region   出去條件
            isIdle = false;
            #endregion

        }

        /// <summary>
        /// 走路 隨機時間
        /// </summary>
        private void Walk()
        {
            #region  持續執行區域
            if (!targetIsDead && PlayerInTrackRange) state = StateEmeny.Track;       //如果玩家進入追蹤圍內 切換追蹤狀態
            nma.SetDestination(v3RandomWalkFinal);                          //代理器 ， 設定目的地(座標)
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.1f);   //走路動畫-離目標距離大於0.1時候走路
            #endregion

            #region  進入狀態
            if (isWalk) return;
            isWalk = true;
            #endregion

            NavMeshHit hit;                                                             //導覽網格碰撞-儲存碰撞資訊
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack,NavMesh.AllAreas); //導覽網格 -取的座標(隨機座標，碰撞資訊，半徑，區域)-網格可行走座標
            v3RandomWalkFinal = hit.position;                                           //最終座標 = 碰撞資訊的座標

            ani.SetBool(parameterIdleWalk, true);
            StartCoroutine(WalkEffect());
        }

        private IEnumerator WalkEffect()
        {
            float randomWalk = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWalk);
            state = StateEmeny.Idle;   //等待狀態

            #region   出去條件
            isWalk = false;
            #endregion

        }

        /// <summary>
        /// 追蹤玩家
        /// </summary>
        private void Track()
        {
            #region
            if (!isTrack)
            {
                StopAllCoroutines();
            }
            isTrack = true;
            #endregion

            nma.isStopped = false;                                                  //導覽器的啟動
            nma.SetDestination(traplayer.position);
            ani.SetBool(parameterIdleWalk, true);
            if (nma.remainingDistance <= rangeAttack) state = StateEmeny.Attack;            //距離判定 攻擊狀態

        }

        /// <summary>
        /// 攻擊玩家
        /// </summary>
        private void Attack()
        {
            nma.isStopped = true;                                       //導覽器 停止
            ani.SetBool(parameterIdleWalk, false);                      //停止走路
            nma.SetDestination(traplayer.position);
            LookAtPlayer();
            if (nma.remainingDistance > rangeAttack) state = StateEmeny.Track;            //距離判定 追蹤狀態
            
            if (isAttack) return;               //如果正在攻擊中 跳出(避免重複攻擊)
            isAttack = true;                    //正在攻擊中

            ani.SetTrigger(paramterAttack);             //攻擊動作

            StartCoroutine(DelaySendDamageToTarget());      //啟動延遲傳送傷害給目標協成
            

        }
        /// <summary>
        /// 延遲傳送傷害給目標
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelaySendDamageToTarget()
        {
            yield return new WaitForSeconds(delaySendDamage);
            //物理 盒形碰撞 (中心點-一半尺寸，角度，圖層)
            Collider[] hits = Physics.OverlapBox(
                transform.position + 
                transform.right * v3AttackOffset.x + 
                transform.up * v3AttackOffset.y + 
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 6
                );
            //if (hits.Length > 0) print(hits[0].name);
            //如果碰撞物件數量是大於0 傳送攻擊力給碰撞物件受傷系統
            if (hits.Length > 0)  targetIsDead= hits[0].GetComponent<HurtSystem>().Hurt(attack);
            if (targetIsDead) TargetDead();
            float waitToNextAttack = timeAttack - delaySendDamage;          //計算剩餘冷卻時間
            yield return new WaitForSeconds(waitToNextAttack);              //等待
            
            isAttack = false;                                               //恢復 攻擊狀態
        }
        private void TargetDead()
        {
            state = StateEmeny.Walk;
            isIdle = false;
            isWalk = false;
            nma.isStopped = false;
        }
        /// <summary>
        /// 面相玩家
        /// </summary>
        private void LookAtPlayer()
        {
            Quaternion angle = Quaternion.LookRotation(traplayer.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speendLookAt);
            ani.SetBool(parameterIdleWalk, transform.rotation != angle);
        }

    }

}
