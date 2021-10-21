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
        /// <summary>
        /// 開始對話
        /// </summary>
        public void Dialogue(DataDialogue data)
        {
            StartCoroutine(SwitchDialogueGroup());          //外部啟動協同程式  所以用公開方法
            StartCoroutine(ShowDialogueContent(data));
        }
        /// <summary>
        /// 顯示對話框
        /// </summary>
        /// <returns></returns>
        private IEnumerator SwitchDialogueGroup()
        {
            for(int i = 0; i < 10; i++)
            {
                groupDialogus.alpha += 0.1f;                //群組元件 透明度 遞增
                yield return new WaitForSeconds(0.01f);     //等待時間 0.03秒    


            }
        }
        private IEnumerator ShowDialogueContent(DataDialogue data)
        {
            textContent.text = "";
            textName.text = "";

            for (int i = 0; i < data.beforeMission[0].Length; i++)
            {
                textContent.text += data.beforeMission[0][i];
                yield return new WaitForSeconds(dialogueInterval);
            }

        }
    }



}

