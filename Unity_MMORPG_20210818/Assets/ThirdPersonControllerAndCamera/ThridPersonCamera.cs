using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Win
{


    public class ThridPersonCamera : MonoBehaviour
    {
        #region 欄位
        [Header("目標物件")]
        public Transform target;
        [Header("追蹤進度"), Range(0, 100)]
        public float speedTrack =1.5f;
        [Header("水平左右速度"), Range(0, 100)]
        public float speedTurnHorizontal = 5;
        [Header("垂直上下速度"), Range(0, 100)]
        public float speedTurnVertical = 5;
        [Header(" X 軸上下旋轉限制: 最小與最大值 ")]
        public Vector2 limitAngleX = new Vector2(-0.2f,  0.2f);

        [Header("攝影機在角色前方上下旋轉限制: 最小與最大值 ")]
        public Vector2 limitAngleFormTarget = new Vector2(-0.2f, 0);
        /// <summary>
        /// 攝影機前方的座標
        /// </summary>
        private Vector3 _posForward;
        /// <summary>
        /// 前方的長度
        /// </summary>
        private float lengthForward = 3;
        #endregion

        #region 屬性
        /// <summary>
        /// 取得滑鼠水瓶座標
        /// </summary>
        private float inputMouseX { get => Input.GetAxis("Mouse X"); }
        /// <summary>
        /// 取得滑鼠垂直座標
        /// </summary>
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        /// <summary>
        /// 攝影機的前方座標
        /// </summary>
        public Vector3 posForawrd {
            get
            {
                _posForward = transform.position + transform.forward * lengthForward;
                _posForward.y = target.position.y;
                return _posForward;
            }
        }

        #endregion

        #region 事件
        private void Update()
        {
            TurnCamera();
            LimitAngleXAndZFTarget();
            FreezeAngleZ();
        }

        // 在 Updata 後執行 ，處理 攝影機追蹤行為
        private void LateUpdate()
        {
            Trackarget();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.2f, 0.1f, 0.3f);
            //前方座標 = 此物件座標+此物件前方*長度
            _posForward = transform.position + transform.forward * lengthForward;
            //前方座標.y=目標.座標.y(讓前方座標得高度與目標相同)
            _posForward.y = target.position.y; 
            Gizmos.DrawSphere(_posForward, 0.15f);
        }
        #endregion

        #region 方法

        ///<summary>
        /// 追蹤目標
        ///</summary>

        private void Trackarget()
        {
            Vector3 pasTarget = target.position;                            //取得 目標的座標
            Vector3 pocCamrea = transform.position;                         //取得 攝影機的座標

            //因為每得電腦效能不一樣，要使電腦速度看到都一樣所以我們會使用 速度*時間一幀
            pocCamrea = Vector3.Lerp(pocCamrea , pasTarget , speedTrack*Time.deltaTime);     //攝影機座標 = 差值                                                                             

            transform.position = pocCamrea;                                 //此物件座標 = 攝影機座標
        }

        private void TurnCamera()
        {
            transform.Rotate(                                           
                inputMouseY * Time.deltaTime*speedTurnVertical,          
                inputMouseX * Time.deltaTime*speedTurnHorizontal, 0);
          

        }
        /// <summary>
        /// 限制角度X軸 與在目前前方的z軸
        /// </summary>
        private void LimitAngleXAndZFTarget()
        {
            //print("攝影機的角度資訊: " +transform.rotation);
            Quaternion angle = transform.rotation;                                               // 取得四位元角度
            angle.x = Mathf.Clamp(angle.x, limitAngleX.x, limitAngleX.y);                          // 夾住角度X軸
            angle.z = Mathf.Clamp(angle.z, limitAngleFormTarget.x, limitAngleFormTarget.y);      // 夾住角度z軸

            transform.rotation = angle;                                                          // 更新物件角度
        }
        private void FreezeAngleZ()
        {
            Vector3 angle = transform.eulerAngles;                              // 取得三軸角度
            angle.z = 0;                                                        // 凍結Z軸為零
            transform.eulerAngles = angle;                                      // 更新物件角度
        }





        #endregion


    }
}