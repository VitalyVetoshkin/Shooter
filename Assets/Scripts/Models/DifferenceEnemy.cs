using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class DifferenceEnemy : BaseSceneObject, IDamageable
    {
        [SerializeField, Range(0f, 100f)] 
        private float defence;

        private float resultDamage;

        public bool IsAlive
        {
            get { return health > 0; }
        }
        
        [SerializeField]
        private float health = 100f;

        public float CurrentHealth => health;
        public void ApplyDamage(float damage, Vector3 damageDirection)
        {
            if (!IsAlive) return;

            if (defence == 0) resultDamage = damage;
            else if (defence == 100) resultDamage = 0;
            else resultDamage = (damage / 100) * (100 - defence);

            health -= resultDamage;

            Main.Instance.DamageUI.text = resultDamage.ToString();
            
            if (!IsAlive) Die();
        }

        private void Die()
        {          
            Collider.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}