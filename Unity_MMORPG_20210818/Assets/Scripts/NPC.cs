using UnityEngine;
namespace Wen.Dialogue
{
    ///<summary>
    ///   NPC 系統
    ///   偵測目標是否進入對話範圍
    ///   並且開啟對話系統
    ///</summary>
    public class NPC : MonoBehaviour
    {
        [Header("對話資料")]
        public DataDialogue dataDialogue;
        [Header("相關資料"), Range(0, 10)]
        public float checkPlayerRadius = 3f;
        public GameObject goTip;
        [Range(0, 10)]
        public float speedLookAt = 3;
        private Transform target;
        private bool startDialoguekey { get => Input.GetKeyDown(KeyCode.E); }

        [Header("對話系統")]
        public DialogueSystem dialogueSystem;
        /// <summary>
        /// 目前任務數量
        /// </summary>
        private int countCurrent;
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, checkPlayerRadius);
        }

        private void Update()
        {
            goTip.SetActive(checkPlayer());
            LookAtPLayer();
            startDialogue();
        }

        /// <summary>
        /// 檢查玩家是否進入 進入後記錄變形資訊
        /// </summary>
        /// <returns></returns>
        private bool checkPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, checkPlayerRadius, 1 << 6);
            //print("進入範圍物件" + hit[0].name);
            if (hits.Length > 0) target = hits[0].transform;
            return hits.Length > 0;
        }
        /// <summary>
        ///  面向玩家
        /// </summary>
        private void LookAtPLayer()
        {
            if(checkPlayer())
            {
                Quaternion angle = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            }
        }

        /// <summary>
        /// 玩家進入範圍內 並且按下指定按鈕 請對話系統執行 開始對話
        /// </summary>
        private void startDialogue()
        {
            if (checkPlayer() && startDialoguekey)
            {
                dialogueSystem.Dialogue(dataDialogue);
            }
            else if (!checkPlayer()) dialogueSystem.StopDialogue();
        }
            /// <summary>
            /// 更新任務需求數量
            /// 任務目標物件得到或死亡後處理
            /// </summary>
        public void UpdataMissionCount()
        {
            countCurrent++;
            // 目前數量 等於 需求數量 狀態 等於 完成任務
            if (countCurrent == dataDialogue.countNeed) dataDialogue.stateNPCMission = StateNPCMission.AfterMission;
        }

    }
}
