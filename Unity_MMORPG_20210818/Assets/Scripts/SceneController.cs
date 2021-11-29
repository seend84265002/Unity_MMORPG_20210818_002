using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wen
{
    /// <summary>
    /// 場景控制
    /// 指定要前往的那個場景
    /// 離開遊戲
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// 載入指定場景
        /// </summary>
        /// <param name="nameScene">場景名稱</param>
        public void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
        /// <summary>
        /// 離開遊戲
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }

}
