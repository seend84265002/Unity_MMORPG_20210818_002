using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace Wen
{
    /// <summary>
    /// �C�������޲z��
    /// �����B�z
    /// 1.���ȧ���
    /// 2.���a���`
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region ���
        [Header("�s�ժ���")]
        public CanvasGroup groupFinal;
        [Header("�����e�����D")]
        public Text textTitle;

        private string titleWin = "You Win";
        private string titleLose = "You Failed";


        #endregion
        #region ��k ���}
        /// <summary>
        /// �}�l�H�J�̫ᤶ��
        /// </summary>
        /// <param name="win">�ϧ_���</param>
        public void StartFadeFinalUI(bool win)
        {
            StartCoroutine(FadeFinalUI( win ? titleWin:titleLose));
        }
        #endregion

        #region ��k �p�H
        private IEnumerator FadeFinalUI(string title)
        {
            textTitle.text = title;
            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;

            for (int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.02f);

            }
        }
        #endregion

    }
}

