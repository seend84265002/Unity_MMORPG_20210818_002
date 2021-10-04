using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Win
{


    public class ThridPersonCamera : MonoBehaviour
    {
        #region ���
        [Header("�ؼЪ���")]
        public Transform target;
        [Header("�l�ܶi��"), Range(0, 100)]
        public float speedTrack =1.5f;
        [Header("�������k�t��"), Range(0, 100)]
        public float speedTurnHorizontal = 5;
        [Header("�����W�U�t��"), Range(0, 100)]
        public float speedTurnVertical = 5;
        #endregion

        #region �ݩ�
        /// <summary>
        /// ���o�ƹ����~�y��
        /// </summary>
        private float inputMouseX { get => Input.GetAxis("Mouse X"); }
        /// <summary>
        /// ���o�ƹ������y��
        /// </summary>
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region �ƥ�
        // �b Updata ����� �A�B�z ��v���l�ܦ欰
        private void LateUpdate()
        {
            Trackarget();
        }
        #endregion

        #region ��k
        private void Update()
        {
            TurnCamera();
        }
        ///<summary>
        /// �l�ܥؼ�
        ///</summary>

        private void Trackarget()
        {
            Vector3 pasTarget = target.position;                            //���o �ؼЪ��y��
            Vector3 pocCamrea = transform.position;                         //���o ��v�����y��

            //�]���C�o�q���įण�@�ˡA�n�Ϲq���t�׬ݨ쳣�@�˩ҥH�ڭ̷|�ϥ� �t��*�ɶ��@�V
            pocCamrea = Vector3.Lerp(pocCamrea , pasTarget , speedTrack*Time.deltaTime);     //��v���y�� = �t��                                                                             

            transform.position = pocCamrea;                                 //������y�� = ��v���y��
        }

        private void TurnCamera()
        {
            transform.Rotate(
                inputMouseY* Time.deltaTime*speedTurnVertical,
                inputMouseX * Time.deltaTime*speedTurnHorizontal, 0);
        }
        #endregion


    }
}