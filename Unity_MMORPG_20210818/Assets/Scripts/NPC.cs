using UnityEngine;
namespace Wen.Dialogue
{
    ///<summary>
    ///   NPC �t��
    ///   �����ؼЬO�_�i�J��ܽd��
    ///   �åB�}�ҹ�ܨt��
    ///</summary>
    public class NPC : MonoBehaviour
    {
        [Header("��ܸ��")]
        public DataDialogue dataDialogue;
        [Header("�������"), Range(0, 10)]
        public float checkPlayerRadius = 3f;
        public GameObject goTip;
        [Range(0, 10)]
        public float speedLookAt = 3;
        private Transform target;
        private bool startDialoguekey { get => Input.GetKeyDown(KeyCode.E); }

        [Header("��ܨt��")]
        public DialogueSystem dialogueSystem;
        /// <summary>
        /// �ثe���ȼƶq
        /// </summary>
        private int countCurrent;
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, checkPlayerRadius);
        }

        private void Update()
        {
            goTip.SetActive(checkPlayer());
            LookAtPLayer();
            startDialogue();
        }

        /// <summary>
        /// �ˬd���a�O�_�i�J �i�J��O���ܧθ�T
        /// </summary>
        /// <returns></returns>
        private bool checkPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, checkPlayerRadius, 1 << 6);
            //print("�i�J�d�򪫥�" + hit[0].name);
            if (hits.Length > 0) target = hits[0].transform;
            return hits.Length > 0;
        }
        /// <summary>
        ///  ���V���a
        /// </summary>
        private void LookAtPLayer()
        {
            if(checkPlayer())
            {
                Quaternion angle = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            }
        }

        /// <summary>
        /// ���a�i�J�d�� �åB���U���w���s �й�ܨt�ΰ��� �}�l���
        /// </summary>
        private void startDialogue()
        {
            if (checkPlayer() && startDialoguekey)
            {
                dialogueSystem.Dialogue(dataDialogue);
            }
            else if (!checkPlayer()) dialogueSystem.StopDialogue();
        }
            /// <summary>
            /// ��s���ȻݨD�ƶq
            /// ���ȥؼЪ���o��Φ��`��B�z
            /// </summary>
        public void UpdataMissionCount()
        {
            countCurrent++;
            // �ثe�ƶq ���� �ݨD�ƶq ���A ���� ��������
            if (countCurrent == dataDialogue.countNeed) dataDialogue.stateNPCMission = StateNPCMission.AfterMission;
        }

    }
}
