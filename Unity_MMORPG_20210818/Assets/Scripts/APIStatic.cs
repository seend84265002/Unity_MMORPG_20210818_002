using UnityEngine;
/// <summary>
/// �{��API�R�A Static
/// </summary>
public class APIStatic : MonoBehaviour
{
    private void Start()
    {
        #region   �R�A�ݩ�
        //���o     Get Set       
        //�y�k
        //���O�W��.�R�A�ݩ�
        //��o�_�������v 
        float a = Random.value;
        print("�����R�A�ݩʡA�H���� : " + a);

        //���o    Set       
        //�y�k
        //���O�W��.�R�A�ݩ� ���w ��;
        //�u�n�ݨ� Read Only �N�O����]�w (��Ū����]�w)
        //Cursor.visible = false;

        #endregion


        #region   �R�A��k
        //�I�s: �Ѽ� �Ǧ^           
        //ñ��: �Ѽ� �Ǧ^
        //�y�k 
        //���O�W��.�R�A��k(�����޼�)
        float range = Random.Range (10.5f, 20.9f);
        print("�H���d��  10.5 - 20.9 : " + range);
        //API �����ܭ��n �ϥξ�ƬO���]�t�̤j��
        int rangeInt = Random.Range(1, 3);
        print("�H���d�� 1-3 (���]�t3) : " + rangeInt);
        #endregion
    }
    private void Update()
    {
        #region  �R�A�ݩ�
        //�ɶ��Ĥ@��0.02�� �A�Ψӭp��(����)�L�����ɶ� �A�����������ɶ�
        //print("�g�L�h�[:" + Time.timeSinceLevelLoad);
        #endregion

        #region  �R�A��k
        float h = Input.GetAxis("Horizontal");
        print("������ : " + h);
        #endregion
    }

}
