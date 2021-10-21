using System.Collections; //�ޥ� �t�� �A���X �R�W�Ŷ� �ΦP�{�� API
using UnityEngine;
namespace Wen.Practice
{
    /// <summary>
    /// �{�Ѩ�P�{�� Coroutine
    /// </summary>
    public class LeamCoroutine : MonoBehaviour
    {
        //�w�q��P�{�Ǥ�k
        private IEnumerator TestCoroutine()
        {
            print("��P�{�Ƕ}�l����");
            yield return new WaitForSeconds(2);   //���d���  �{���e���@�w�n�[ yield
            print("��P�{�ǵ���2������");
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
            //�Ұʨ�P�{��
            StartCoroutine(TestCoroutine());    //StartCoroutiner �~�� MonoBehaviour����k  �i�H�����ϥ�
            StartCoroutine("SpherScale");
        }

    }

}
