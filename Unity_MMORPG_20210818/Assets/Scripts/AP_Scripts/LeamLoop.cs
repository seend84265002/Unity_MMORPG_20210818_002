
using UnityEngine;
namespace Wen.Practice {
    /// <summary>
    /// �{�Ѱj��
    /// while �Ado while�Afor�Aforeach 
    /// </summary>
    public class LeamLoop : MonoBehaviour
    {
        private void Start()
        {
            //�j�� Loop
            //���ư���{�����e
            //�ݨD : ��X�Ʀr 1-5
            //while �j��
            //�y�k if(���L��) {�{�����e}  -  ���L�� true ����@��
            //�y�k while(���L��) {�{�����e}  -  ���L�� true �������
            int a = 1;
            while (a < 6)
            {
                print("�j�� while:" + a);
                a++;
            }
            //for �j��
            //�y�k for (��l�� ; ����(���L��) ; �j�鵲������{��) {�{�����e}
            for(int i = 1; i < 10; i++)
            {
                print("�j�� for:" + i);
            }


        }
    }
}

