using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace Wen
{
    /// <summary>
    /// 遊戲結束管理器
    /// 結束處理
    /// 1.任務完成
    /// 2.玩家死亡
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region 欄位
        [Header("群組物件")]
        public CanvasGroup groupFinal;
        [Header("結束畫面標題")]
        public Text textTitle;

        private string titleWin = "You Win";
        private string titleLose = "You Failed";


        #endregion
        #region 方法 公開
        /// <summary>
        /// 開始淡入最後介面
        /// </summary>
        /// <param name="win">使否獲勝</param>
        public void StartFadeFinalUI(bool win)
        {
            StartCoroutine(FadeFinalUI( win ? titleWin:titleLose));
        }
        #endregion

        #region 方法 私人
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

