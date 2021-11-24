using UnityEngine;
using UnityEngine.Events;
namespace Wen
{
    public class HurtSystem : MonoBehaviour
    {
        /// <summary>
        /// ���˨t��
        /// �B�z��q�A���˻P���`
        /// </summary>
        #region ��� ���}
        [Header("��q"), Range(0, 5000)]
        public float hp = 100;
        [Header("���˨ƥ�")]
        public UnityEvent onHurt;
        [Header("���`�ƥ�")]
        public UnityEvent onDead;
        [Header("�ʵe�Ѽ�:���˻P���`")]
        public string parameterHurt = "����Ĳ�o";
        public string paramrterDead = "���`�}��";
        #endregion

        #region ��� �p�H�P�O�@
        //private �p�H �����\�b�l���O�s��
        //public ���} ���i�s��
        //protected �O�@ �ȭ��l���O�s��
        protected float hpMax;    
        private Animator ani;
        #endregion
        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
            hpMax = hp;
        }
        #endregion

        #region ��k ���}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="damage">�����쪺�ˮ`</param>
        //  �����n�Q�l���O�Ƽg�����[�W virtual ����
        public virtual void Hurt(float damage)
        {
            if (ani.GetBool(paramrterDead)) return;     //�p�G���` �ѼƤw�g�Ŀ�N���X
            hp -= damage;
            ani.SetTrigger(parameterHurt);
            onHurt.Invoke();
            if (hp <= 0) Dead();

        }
        #endregion
        #region ��k �p�H
        /// <summary>
        /// ���`
        /// </summary>
        private void Dead()
        {
            ani.SetBool(paramrterDead,true);
            onDead.Invoke();
        }
        #endregion

    }
}

