using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wen.Dialogue
{
    ///<summary>
    /// ��ܨt��
    /// ��ܹ�ܮءA��ܤ��e���r�ĪG
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        #region ���
        [Header("��ܨt�λݭn��������")]
        public CanvasGroup groupDialogus;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("��ܶ��j"), Range(0, 10)]
        public float dialogueInterval = 0.03f;
        [Header("��ܪ�����")]
        public KeyCode dialogueKey = KeyCode.Mouse0;
        [Header("���r�ƥ�")]
        public UnityEvent onType;
        #endregion

        /// <summary>
        /// �}�l���
        /// </summary>
        /// 

        public void Dialogue(DataDialogue data)
        {
            StopAllCoroutines();                            //��ܫe������ ���P�{��    
            StartCoroutine(SwitchDialogueGroup());          //�~���Ұʨ�P�{��  �ҥH�Τ��}��k
            StartCoroutine(ShowDialogueContent(data));
        }

        public void StopDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(SwitchDialogueGroup(false));
        }    
        /// <summary>
        /// ��ܹ�ܮ�
        /// </summary>
        /// <returns name="fadeIn">�O�_true�H�J  false�H�X </returns>
        private IEnumerator SwitchDialogueGroup(bool fadeIn = true)
        {
            //�T���B��l
            //�y�k  ���L�� ? true ���G ?false ���G
            
            float increase = fadeIn ? 0.1f : -0.1f;
            for(int i = 0; i < 10; i++)
            {
                groupDialogus.alpha += increase;                //�s�դ��� �z���� ���W
                yield return new WaitForSeconds(0.01f);         //���ݮɶ� 0.03��    
            }
        }
        /// <summary>
        /// ��ܹ�ܤ��e
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerator ShowDialogueContent(DataDialogue data)
        {
            
            textName.text = "";     //�M�� ��ܪ�
          
            textName.text = data.nameDialouge;      //��s ��ܪ�
            #region �B�z���A�P��ܸ��
            string[] dialogueContents = { };         // �x�s ��ܤ��e

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

            //�M�M�Ĥ@�q���
            for (int j = 0; j < dialogueContents.Length; j++)
            {
                textContent.text = "";  //�M����ܤ��e
                goTriangle.SetActive(false);        //�v�� ���ܹϥ�
                //�M�M��ܨC�@�Ӧr    
                for (int i = 0; i < dialogueContents[j].Length; i++)
                {
                    onType.Invoke();
                    textContent.text += dialogueContents[j][i];
                    yield return new WaitForSeconds(dialogueInterval);
                }
                goTriangle.SetActive(true);         //��� ���ܹϥ�
                // ���򵥫� ��J ��ܫ��� null ���ݤ@�Ӽv�檺�ɶ�
                while (!Input.GetKeyDown(dialogueKey)) yield return null;
            }
            StartCoroutine(SwitchDialogueGroup(false));  //�H�X

        }
    }



}

