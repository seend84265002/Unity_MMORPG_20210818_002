using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wen.Dialogue
{
    ///<summary>
    /// 對話系統
    /// 顯示對話框，對話內容打字效果
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        #region 欄位
        [Header("對話系統需要介面物件")]
        public CanvasGroup groupDialogus;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("對話間隔"), Range(0, 10)]
        public float dialogueInterval = 0.03f;
        [Header("對話的按鍵")]
        public KeyCode dialogueKey = KeyCode.Mouse0;
        [Header("打字事件")]
        public UnityEvent onType;
        #endregion

        /// <summary>
        /// 開始對話
        /// </summary>
        /// 

        public void Dialogue(DataDialogue data)
        {
            StopAllCoroutines();                            //對話前先停止 偕同程序    
            StartCoroutine(SwitchDialogueGroup());          //外部啟動協同程式  所以用公開方法
            StartCoroutine(ShowDialogueContent(data));
        }

        public void StopDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(SwitchDialogueGroup(false));
        }    
        /// <summary>
        /// 顯示對話框
        /// </summary>
        /// <returns name="fadeIn">是否true淡入  false淡出 </returns>
        private IEnumerator SwitchDialogueGroup(bool fadeIn = true)
        {
            //三元運算子
            //語法  布林直 ? true 結果 ?false 結果
            
            float increase = fadeIn ? 0.1f : -0.1f;
            for(int i = 0; i < 10; i++)
            {
                groupDialogus.alpha += increase;                //群組元件 透明度 遞增
                yield return new WaitForSeconds(0.01f);         //等待時間 0.03秒    
            }
        }
        /// <summary>
        /// 顯示對話內容
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerator ShowDialogueContent(DataDialogue data)
        {
            
            textName.text = "";     //清除 對話者
          
            textName.text = data.nameDialouge;      //更新 對話者
            #region 處理狀態與對話資料
            string[] dialogueContents = { };         // 儲存 對話內容

            switch (data.stateNPCMission)
            {
                case StateNPCMission.BeforMission:
                    dialogueContents = data.beforeMission;
                    break;
                case StateNPCMission.Missionning:
                    dialogueContents = data.missionning;
                    break;
                case StateNPCMission.AfterMission:
                    dialogueContents = data.afterMission;
                    break;

            }
            #endregion

            //遍尋第一段對話
            for (int j = 0; j < dialogueContents.Length; j++)
            {
                textContent.text = "";  //清除對話內容
                goTriangle.SetActive(false);        //影藏 提示圖示
                //遍尋對話每一個字    
                for (int i = 0; i < dialogueContents[j].Length; i++)
                {
                    onType.Invoke();
                    textContent.text += dialogueContents[j][i];
                    yield return new WaitForSeconds(dialogueInterval);
                }
                goTriangle.SetActive(true);         //顯示 提示圖示
                // 持續等待 輸入 對話按鍵 null 等待一個影格的時間
                while (!Input.GetKeyDown(dialogueKey)) yield return null;
            }
            StartCoroutine(SwitchDialogueGroup(false));  //淡出

        }
    }



}

