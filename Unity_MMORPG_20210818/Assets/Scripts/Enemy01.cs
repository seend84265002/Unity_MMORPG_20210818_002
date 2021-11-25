using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Wen.Dialogue;

namespace Wen.Enemy
{
    /// <summary>
    /// �ĤH�欰
    /// �ĤH���A�G���ݡB�����B�l�ܡB�����B���ˡB���`
    /// </summary>
    public class Enemy01 : MonoBehaviour
    {
        #region ���G���}
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35;
        [Header("�d��G�l�ܻP����")]
        [Range(0, 7)]
        public float rangeAttack = 5;
        [Range(7, 20)]
        public float rangeTrack = 15;
        [Header("�����H�����")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("�����H�����")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);
        [Header("�����ϰ�첾�P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        [Header("�����N�o�ɶ�"), Range(0, 5)]
        public float timeAttack = 2.5f;
        [Header("��������ǰe�ˮ`�ɶ�"), Range(0, 5)]
        public float delaySendDamage = 0.5f;
        [Header("���V���a�t��"), Range(0, 50)]
        public float speedLookAt = 10;
        #endregion

        #region ���G�p�H
        [SerializeField]    // �ǦC�����G��ܨp�H���
        private StateEmeny state;
        /// <summary>
        /// �O�_���ݪ��A
        /// </summary>
        private bool isIdle;
        /// <summary>
        /// �O�_�������A
        /// </summary>
        private bool isWalk;
        /// <summary>
        /// �H���樫�y�СG�z�L API ���o���椺�i���쪺��m
        /// </summary>
        private Vector3 v3RandomWalkFinal;
        private Animator ani;
        private NavMeshAgent nma;
        private string parameterIdleWalk = "�����}��";

        /// <summary>
        /// �H���樫�y��
        /// </summary>
        private Vector3 v3RandomWalk
        {
            get => Random.insideUnitSphere * rangeTrack + transform.position;
        }
        /// <summary>
        /// ���a�O�_�b�l�ܽd�򤺡Atrue �O�Afalse �_
        /// </summary>
        private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }
        private Transform traPlayer;
        private string namePlayer = "�k�D��";
        private bool isTrack;
        private string parameterAttack = "����Ĳ�o";
        private bool isAttack;
        private bool targetIsDead;
        #endregion

        #region ø�s�ϧ�
        private void OnDrawGizmos()
        {
            #region �����d��B�l�ܽd��P�H���樫�y��
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);

            if (state == StateEmeny.Walk)
            {
                Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }
            #endregion

            #region �����I���P�w�ϰ�
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);

            // ø�s��ΡA�ݭn��ۨ������ɽШϥ� matrix ���w�y�Ш��׻P�ؤo
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position + 
                transform.right * v3AttackOffset.x + 
                transform.up * v3AttackOffset.y + 
                transform.forward * v3AttackOffset.z, 
                transform.rotation, transform.localScale);

            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion
        }
        #endregion

        #region �ƥ�
        [Header("NPC �W��")]
        public string nameNPC = "NPC �p��";

        private NPC npc;
        private HurtSystem hurtSystem;

        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = speed;
            hurtSystem = GetComponent<HurtSystem>();

            traPlayer = GameObject.Find(namePlayer).transform;
            npc = GameObject.Find(nameNPC).GetComponent<NPC>();

            // ���˨t�� - ���`�ƥ�Ĳ�o�� �� NPC ��s�ƶq
            // AddListener(��k) �K�[��ť��(��k)
            hurtSystem.onDead.AddListener(npc.UpdataMissionCount);

            nma.SetDestination(transform.position);             // ������ �@�}�l�N���Ұ�
        }

        private void Update()
        {
            StateManager();
        }
        #endregion

        #region ��k�G�p�H
        /// <summary>
        /// ���A�޲z
        /// </summary>
        private void StateManager()
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
        /// ���ݡG�H����ƫ�i���������A
        /// </summary>
        private void Idle()
        {
            if (!targetIsDead && playerInTrackRange) state = StateEmeny.Track;           // �p�G ���a�i�J �l�ܽd�� �N�����l�ܪ��A

            #region �i�J����
            if (isIdle) return;
            isIdle = true;
            #endregion

            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(IdleEffect());
        }

        /// <summary>
        /// ���ݮĪG
        /// </summary>
        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);

            state = StateEmeny.Walk;        // �i�J�������A

            #region �X�h����
            isIdle = false;
            #endregion
        }

        /// <summary>
        /// �����G�H����ƫ�i�����ݪ��A
        /// </summary>
        private void Walk()
        {
            #region �������ϰ�
            if (!targetIsDead && playerInTrackRange) state = StateEmeny.Track;           // �p�G ���a�i�J �l�ܽd�� �N�����l�ܪ��A

            nma.SetDestination(v3RandomWalkFinal);                                          // �N�z��.�]�w�ت��a(�y��)
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.05f);                   // �����ʵe - ���ت��a�Z���j�� 0.1 �ɨ���
            #endregion

            #region �i�J����
            if (isWalk) return;
            isWalk = true;
            #endregion

            NavMeshHit hit;                                                                 // ��������I�� - �x�s����I����T
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack, NavMesh.AllAreas);    // ��������.���o�y��(�H���y�СA�I����T�A�b�|�A�ϰ�) - ���椺�i�樫���y��
            v3RandomWalkFinal = hit.position;                                               // �̲׮y�� = �I����T �� �y��
            
            StartCoroutine(WalkEffect());
        }

        /// <summary>
        /// �����ĪG
        /// </summary>
        private IEnumerator WalkEffect()
        {
            float randomWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            yield return new WaitForSeconds(randomWalk);

            state = StateEmeny.Idle;

            #region ���}����
            isWalk = false;
            #endregion
        }

        /// <summary>
        /// �l�ܪ��a
        /// </summary>
        private void Track()
        {
            #region �i�J����
            if (!isTrack)
            {
                StopAllCoroutines();
            }

            isTrack = true;
            #endregion

            nma.isStopped = false;                          // ������ �Ұ�
            nma.SetDestination(traPlayer.position);
            ani.SetBool(parameterIdleWalk, true);

            // �Z���p�󵥩���� �N�� �������A
            if (nma.remainingDistance <= rangeAttack) state = StateEmeny.Attack;
        }

        /// <summary>
        /// �������a
        /// </summary>
        private void Attack()
        {
            nma.isStopped = true;                           // ������ ����
            ani.SetBool(parameterIdleWalk, false);          // �����
            nma.SetDestination(traPlayer.position);
            LookAtPlayer();

            if (nma.remainingDistance > rangeAttack) state = StateEmeny.Track;

            if (isAttack) return;                       // �p�G ���b������ �N���X (�קK���Ƨ���)
            isAttack = true;                            // ���b ������

            ani.SetTrigger(parameterAttack);

            StartCoroutine(DelaySendDamageToTarget());  // �Ұʩ���ǰe�ˮ`���ؼШ�{
        }

        /// <summary>
        /// ����ǰe�ˮ`���ؼ�
        /// </summary>
        private IEnumerator DelaySendDamageToTarget()
        {
            yield return new WaitForSeconds(delaySendDamage);

            // ���z ���θI��(�����I�A�@�b�ؤo�A���סA�ϼh)
            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 6);

            // �p�G �I������ƶq�j�� �s�A�ǰe�����O���I�����󪺨��˨t��
            if (hits.Length > 0) targetIsDead = hits[0].GetComponent<HurtSystem>().Hurt(attack);
            if (targetIsDead) TargetDead();

            float waitToNextAttack = timeAttack - delaySendDamage;          // �p��Ѿl�N�o�ɶ�
            yield return new WaitForSeconds(waitToNextAttack);              // ����

            isAttack = false;                                               // ��_ �������A
        }

        /// <summary>
        /// �ؼЦ��`
        /// </summary>
        private void TargetDead()
        {
            state = StateEmeny.Walk;
            isIdle = false;
            isWalk = false;
            nma.isStopped = false;
        }

        /// <summary>
        /// ���V���a
        /// </summary>
        private void LookAtPlayer()
        {
            Quaternion angle = Quaternion.LookRotation(traPlayer.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            //ani.SetBool(parameterIdleWalk, transform.rotation != angle);
            ani.SetBool(parameterIdleWalk, Quaternion.Dot(transform.rotation, angle) < 0.9f);
        }
        #endregion
    }
}
