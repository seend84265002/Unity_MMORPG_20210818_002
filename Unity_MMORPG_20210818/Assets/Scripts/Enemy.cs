using System.Collections; //�I�s�󦨵{��
using UnityEngine;
using UnityEngine.AI;
namespace Wen.Enemy
{
    /// <summary>
    /// �ĤH�欰
    /// �ĤH���A : ���� ���� �l�� ���� ���� ���`
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region ��� ���}
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35;
        [Header("�d��:�l�ܻP����")]
        [Range(0,7)]
        public float rangeAttack = 5;
        [Range(7, 20)]
        public float rangeTrack = 15;
        [Header("�����H�����")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("�����H�����")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);
        [Header("�����ϰ� �첾 �P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        [Header("�����ɶ�"), Range(0, 5)]
        public float timeAttack=2.5f;
        #endregion

        #region ��� �p�H
        [SerializeField]        //�ǦC�����  SerializeField�i�H��ܨp�H���
        private StateEmeny state;
        private NavMeshAgent nma;
        private Animator ani;
        private string parameterIdleWalk ="�����}��";
        /// <summary>
        /// �H���樫�y��
        /// </summary>
        private Vector3 v3RandomWalk
        {
            get => Random.insideUnitSphere * rangeTrack + transform.position;
        }
        private Vector3 v3RandomWalkFinal;   //�̲׮y��(�B�⧹�᪺)
        /// <summary>
        /// �ˬd���a�O�_�b�l�ܽd��
        /// </summary>
        private bool PlayerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }
        private Transform traplayer;
        private string nameplayer = "�k�D��";
        
        private bool isIdle; //�O�_���ݪ��A
        private bool isWalk; //�O�_�������A
        private bool isTrack; //�ϧ_�l�ܪ��A
        private string paramterAttack="����Ĳ�o";
        private bool isAttack;
        #endregion


        #region ø�s�ϣ���
        private void OnDrawGizmos()
        {
            #region �����d�� �l�ܽd�� �P�H���樫�y��
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);
            if (state == StateEmeny.Walk)
            {
                Gizmos.color = new Color(1f, 0, 0.2f, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }
            #endregion

            #region �����I���P�w�ϰ�
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);

            //ø�s��ΡA�ݭn��̨������ɽШϥ� matrix ���w�y�Ш��׻P�ؤo
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position+transform.right*v3AttackOffset.x+
                transform.up*v3AttackOffset.y+transform.forward*v3AttackOffset.z, 
                transform.rotation,
                transform.localScale);    //��������

            Gizmos.DrawCube(Vector3.one, v3AttackSize);
            #endregion




        }
        #endregion

        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();

            traplayer = GameObject.Find(nameplayer).transform;
            nma.SetDestination(transform.position);             //������ �@�}�l�N���ʧ@
        }

        private void Update()
        {
            StartManager();
        }

        /// <summary>
        /// ���A�޲z
        /// </summary>
        private void StartManager()
        {
            switch (state)
            {
                case StateEmeny.Idle:
                    Idle();
                    break;
                case StateEmeny.Walk:
                    Walk();
                    break;
                case StateEmeny.Track:
                    Track();
                    break;
                case StateEmeny.Attack:
                    Attack();
                    break;
                case StateEmeny.Hurt:
                    break;
                case StateEmeny.Dead:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ���� �H����ƨ������A
        /// </summary>
        private void Idle()
        {
            if (PlayerInTrackRange) state = StateEmeny.Track;       //�p�G���a�i�J�l�ܳ� �����l�ܪ��A
            #region  �i�J���A
            if (isIdle) return;
            isIdle = true;
            #endregion

            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(IdleEffect());
        }

        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);
            state = StateEmeny.Walk;   //�������A

            #region   �X�h����
            isIdle = false;
            #endregion

        }
        /// <summary>
        /// ���� �H���ɶ�
        /// </summary>
        private void Walk()
        {
            #region  �������ϰ�
            if (PlayerInTrackRange) state = StateEmeny.Track;       //�p�G���a�i�J�l�ܳ� �����l�ܪ��A
            nma.SetDestination(v3RandomWalkFinal);                          //�N�z�� �A �]�w�ت��a(�y��)
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.1f);   //�����ʵe-���ؼжZ���j��0.1�ɭԨ���
            #endregion

            #region  �i�J���A
            if (isWalk) return;
            isWalk = true;
            #endregion

            NavMeshHit hit;                                                             //��������I��-�x�s�I����T
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack,NavMesh.AllAreas); //�������� -�����y��(�H���y�СA�I����T�A�b�|�A�ϰ�)-����i�樫�y��
            v3RandomWalkFinal = hit.position;                                           //�̲׮y�� = �I����T���y��

            ani.SetBool(parameterIdleWalk, true);
            StartCoroutine(WalkEffect());
        }

        private IEnumerator WalkEffect()
        {
            float randomWalk = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWalk);
            state = StateEmeny.Idle;   //���ݪ��A

            #region   �X�h����
            isWalk = false;
            #endregion

        }
        /// <summary>
        /// �l�ܪ��a
        /// </summary>

        private void Track()
        {
            #region
            if (!isTrack)
            {
                StopAllCoroutines();
            }
            isTrack = true;
            #endregion

            nma.isStopped = false;                                                  //���������Ұ�
            nma.SetDestination(traplayer.position);
            ani.SetBool(parameterIdleWalk, true);
            if (nma.remainingDistance <= rangeAttack) state = StateEmeny.Attack;            //�Z���P�w �������A

        }
        private void Attack()
        {
            nma.isStopped = true;                                       //������ ����
            ani.SetBool(parameterIdleWalk, false);                      //�����
            nma.SetDestination(traplayer.position);
            if (nma.remainingDistance > rangeAttack) state = StateEmeny.Track;            //�Z���P�w �l�ܪ��A
            
            if (isAttack) return;
            ani.SetTrigger(paramterAttack);             //�����ʧ@

            Collider[] hits = Physics.OverlapBox(
                transform.position + transform.right * v3AttackOffset.x + transform.up * v3AttackOffset.y + transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 6
                    );
            if (hits.Length > 0) print("�����쪺����:" + hits[0].name);
            isAttack = true;

        }

    }

}
