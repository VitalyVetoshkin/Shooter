using UnityEngine;

namespace FPS
{
    public class TestEnemy : BaseEnemy, IDamageable
    {
        public void ApplyDamage(float damage, Vector3 damageDirection)
        {
            base.ApplyDamage(damage, damageDirection);
            
            health -= damage;
            
            Main.Instance.DamageUI.text = damage.ToString();
            
            Color = Random.ColorHSV();
            Rigidbody.AddForce(damageDirection, ForceMode.Impulse);
        }

        public void Die()
        {
            base.Die();
            
            Color = Color.red;
        }
    }
}