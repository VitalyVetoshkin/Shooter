using UnityEngine;

namespace FPS
{
    public class TestEnemy : BaseSceneObject, IDamageable
    {
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

            health -= damage;
            Main.Instance.DamageUI.text = damage.ToString();
            
            Color = Random.ColorHSV();
            Rigidbody.AddForce(damageDirection, ForceMode.Impulse);

            if (!IsAlive) Die();
        }

        private void Die()
        {
            Color = Color.red;
            Collider.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}