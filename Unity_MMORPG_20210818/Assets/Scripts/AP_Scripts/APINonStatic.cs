using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �{��API �D�R�A Non static
/// </summary>
public class APINonStatic : MonoBehaviour
{
    public Transform tra1;          //�׹��� �n�s���D�R�A�����O ���W��;  ����N�OCamera ...�� Unity���骫��C
    public Camera cam;
    public Light lig;
    private void Start()
    {
        #region �D�R�A�ݩ�
        //�ʺA�P�R�A���@��
        //1.�ݭn���骫��
        //2.�������骫��A�w�q���ñN�n������s�J���
        //3.�C������A���󥲶��s�b��������
        //���o Get
        //�y�k:���W�١A�D�R�A�ݩ�
        print("��v�����y�� : " + tra1.position);
        print("��v�����`�� : " + cam.depth);

        //�]�w Set
        //�y�k : ���W�� .�D�R�A�ݩ� ���w��:

        tra1.position = new Vector3(55, 66, 77);
        cam.depth = 7;
        print("��v�����y�� : " + tra1.position);
        print("��v�����`�� : " + cam.depth);
        #endregion

        #region �D�R�A��k
        //�I�s 
        //�y�k
        //���W�١A�D�R�A��k�W��(�����Ѽ�):
        lig.Reset();



        #endregion
    }
}
