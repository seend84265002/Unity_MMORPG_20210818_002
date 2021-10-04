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
        #endregion

        #region 事件
        // 在 Updata 後執行 ，處理 攝影機追蹤行為
        private void LateUpdate()
        {
            Trackarget();
        }
        #endregion

        #region 方法
        private void Update()
        {
            TurnCamera();
        }
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
                inputMouseY* Time.deltaTime*speedTurnVertical,
                inputMouseX * Time.deltaTime*speedTurnHorizontal, 0);
        }
        #endregion


    }
}