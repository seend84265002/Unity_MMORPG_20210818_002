using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIStaticPractice : MonoBehaviour
{
  
    private void Start()
    {
        print("�Ҧ���v�����ƶq : " + Camera.allCamerasCount);

        print("2D�����O�j�p : " + Physics2D.gravity);

        print("��P�v : " + Mathf.PI.ToString("0.000"));
        
        Physics2D.gravity = new Vector2(0, -20);
                           
        Time.timeScale = 0.5f;
       
        print("��9.999�h�p���I : " + Mathf.FloorToInt(9.999f));

        Vector3 one = new Vector3(1, 1, 1);
        Vector3 two = new Vector3(22, 22, 22);
        print("���o���I���Z�� : " + Vector3.Distance(one, two));

        //�}�ҳs�� 
        Application.OpenURL("https://unity.com/");
    }

 
    private  void Update()
    {
        print("�O�_��J���N�� : " + Input.anyKey);

        print("�C���g�L���ɶ� : " + Time.timeSinceLevelLoad); //Time.time �Ҧ������ɶ����|�p��(���|�������ɶ��N�q�s�p��)

        print("�O�_���U�ťհ� :" + Input.GetKeyDown(KeyCode.Space));
    }
}
