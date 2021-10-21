using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Wen.Dialogue
{
    ///<summary>
    /// ��ܨt��
    /// ��ܹ�ܮءA��ܤ��e���r�ĪG
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [Header("��ܨt�λݭn��������")]
        public CanvasGroup groupDialogus;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("��ܶ��j"), Range(0, 10)]
        public float dialogueInterval = 0.03f;
        /// <summary>
        /// �}�l���
        /// </summary>
        public void Dialogue(DataDialogue data)
        {
            StartCoroutine(SwitchDialogueGroup());          //�~���Ұʨ�P�{��  �ҥH�Τ��}��k
            StartCoroutine(ShowDialogueContent(data));
        }
        /// <summary>
        /// ��ܹ�ܮ�
        /// </summary>
        /// <returns></returns>
        private IEnumerator SwitchDialogueGroup()
        {
            for(int i = 0; i < 10; i++)
            {
                groupDialogus.alpha += 0.1f;                //�s�դ��� �z���� ���W
                yield return new WaitForSeconds(0.01f);     //���ݮɶ� 0.03��    


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

