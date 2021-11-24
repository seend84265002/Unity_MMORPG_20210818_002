using UnityEngine.UI;
using UnityEngine;
using System.Collections;
namespace Wen
{
    /// <summary>
    /// 繼承受傷系統
    /// 包含介面的受傷系統
    /// 可以處理血條更新
    /// </summary>
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("需要更新的血條")]
        public Image imgHp;
        /// <summary>
        /// 血條效果專用 扣血前的血量
        /// </summary>
        private float hpEffectOriginal;
        // 複寫 父類別 成員 override
        public override bool Hurt(float damage)
        {
            hpEffectOriginal = hp;
            base.Hurt(damage);      //該成員的父類別基底 父類別內的內容
            StartCoroutine(HpBarEffect());
            return hp <= 0;
        }
        private IEnumerator HpBarEffect()
        {
            while(hpEffectOriginal != hp)                       //當 扣血前血量不等於血量
            {
                hpEffectOriginal--;                             //遞減
                imgHp.fillAmount = hpEffectOriginal / hpMax;    //更新血條    
                yield return new WaitForSeconds(0.01f);         //等待

            }
            
        }
    }

}


