using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Wen.Dialogue
{
    ///<summary>
    /// 對話系統
    /// 顯示對話框，對話內容打字效果
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [Header("對話系統需要介面物件")]
        public CanvasGroup groupDialogus;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("對話間隔"), Range(0, 10)]
        public float dialogueInterval = 0.03f;
        [Header("對話的按鍵")]
        public KeyCode dialogueKey = KeyCode.Mouse0;
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
                yield return new WaitForSeconds(0.01f);     //等待時間 0.03秒    
            }
        }
        private IEnumerator ShowDialogueContent(DataDialogue data)
        {
            
            textName.text = "";     //清除對話者
          
            textName.text = data.nameDialouge;

            string[] dialogueContents = data.beforeMission;
            goTriangle.SetActive(false);        //影藏 提示圖示
            //遍尋第一段對話
            for (int j = 0; j < dialogueContents.Length; j++)
            {
                textContent.text = "";  //清除對話內容
                //遍尋對話每一個字    
                for (int i = 0; i < dialogueContents[j].Length; i++)
                {
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

