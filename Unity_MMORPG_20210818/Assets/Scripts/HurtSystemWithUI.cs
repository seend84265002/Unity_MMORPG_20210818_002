using UnityEngine.UI;
using UnityEngine;
using System.Collections;
namespace Wen
{
    /// <summary>
    /// �~�Ө��˨t��
    /// �]�t���������˨t��
    /// �i�H�B�z�����s
    /// </summary>
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("�ݭn��s�����")]
        public Image imgHp;
        /// <summary>
        /// ����ĪG�M�� ����e����q
        /// </summary>
        private float hpEffectOriginal;
        // �Ƽg �����O ���� override
        public override bool Hurt(float damage)
        {
            hpEffectOriginal = hp;
            base.Hurt(damage);      //�Ӧ����������O�� �����O�������e
            StartCoroutine(HpBarEffect());
            return hp <= 0;
        }
        private IEnumerator HpBarEffect()
        {
            while(hpEffectOriginal != hp)                       //�� ����e��q�������q
            {
                hpEffectOriginal--;                             //����
                imgHp.fillAmount = hpEffectOriginal / hpMax;    //��s���    
                yield return new WaitForSeconds(0.01f);         //����

            }
            
        }
    }

}


