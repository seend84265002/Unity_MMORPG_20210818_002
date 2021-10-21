using System.Collections; //引用 系統 ，集合 命名空間 形同程序 API
using UnityEngine;
namespace Wen.Practice
{
    /// <summary>
    /// 認識協同程序 Coroutine
    /// </summary>
    public class LeamCoroutine : MonoBehaviour
    {
        //定義協同程序方法
        private IEnumerator TestCoroutine()
        {
            print("協同程序開始執行");
            yield return new WaitForSeconds(2);   //停留兩秒  程式前面一定要加 yield
            print("協同程序等待2秒後執行");
        }

        public Transform sphere;

        private IEnumerator SpherScale()
        {
            for(int i =1; i<10; i++)
            {
                sphere.localScale += Vector3.one;
                yield return new WaitForSeconds(0.2f);
            }
        }

        private void Start()
        {
            //啟動協同程序
            StartCoroutine(TestCoroutine());    //StartCoroutiner 繼承 MonoBehaviour的方法  可以直接使用
            StartCoroutine("SpherScale");
        }

    }

}
