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
        [Header(" X �b�W�U���୭��: �̤p�P�̤j�� ")]
        public Vector2 limitAngleX = new Vector2(-0.2f,  0.2f);

        [Header("��v���b����e��W�U���୭��: �̤p�P�̤j�� ")]
        public Vector2 limitAngleFormTarget = new Vector2(-0.2f, 0);
        /// <summary>
        /// ��v���e�誺�y��
        /// </summary>
        private Vector3 _posForward;
        /// <summary>
        /// �e�誺����
        /// </summary>
        private float lengthForward = 3;
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
        /// <summary>
        /// ��v�����e��y��
        /// </summary>
        public Vector3 posForawrd {
            get
            {
                _posForward = transform.position + transform.forward * lengthForward;
                _posForward.y = target.position.y;
                return _posForward;
            }
        }

        #endregion

        #region �ƥ�
        private void Update()
        {
            TurnCamera();
            LimitAngleXAndZFTarget();
            FreezeAngleZ();
        }

        // �b Updata ����� �A�B�z ��v���l�ܦ欰
        private void LateUpdate()
        {
            Trackarget();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.2f, 0.1f, 0.3f);
            //�e��y�� = ������y��+������e��*����
            _posForward = transform.position + transform.forward * lengthForward;
            //�e��y��.y=�ؼ�.�y��.y(���e��y�бo���׻P�ؼЬۦP)
            _posForward.y = target.position.y; 
            Gizmos.DrawSphere(_posForward, 0.15f);
        }
        #endregion

        #region ��k

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
                inputMouseY * Time.deltaTime*speedTurnVertical,          
                inputMouseX * Time.deltaTime*speedTurnHorizontal, 0);
          

        }
        /// <summary>
        /// �����X�b �P�b�ثe�e�誺z�b
        /// </summary>
        private void LimitAngleXAndZFTarget()
        {
            //print("��v�������׸�T: " +transform.rotation);
            Quaternion angle = transform.rotation;                                               // ���o�|�줸����
            angle.x = Mathf.Clamp(angle.x, limitAngleX.x, limitAngleX.y);                          // ������X�b
            angle.z = Mathf.Clamp(angle.z, limitAngleFormTarget.x, limitAngleFormTarget.y);      // ������z�b

            transform.rotation = angle;                                                          // ��s���󨤫�
        }
        private void FreezeAngleZ()
        {
            Vector3 angle = transform.eulerAngles;                              // ���o�T�b����
            angle.z = 0;                                                        // �ᵲZ�b���s
            transform.eulerAngles = angle;                                      // ��s���󨤫�
        }





        #endregion


    }
}