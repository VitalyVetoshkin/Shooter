using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class DifferenceEnemy : BaseEnemy, IDamageable
    {
        [SerializeField, Range(0f, 100f)] 
        private float defence;

        private float resultDamage;
        
        public void ApplyDamage(float damage, Vector3 damageDirection)
        {
            base.ApplyDamage(damage, damageDirection);
            
            if (defence == 0) resultDamage = damage;
            else if (defence == 100) resultDamage = 0;
            else resultDamage = (damage / 100) * (100 - defence);

            health -= resultDamage;

            Main.Instance.DamageUI.text = resultDamage.ToString();
        }

        public void Die()
        {          
            base.Die();
        }
    }
}