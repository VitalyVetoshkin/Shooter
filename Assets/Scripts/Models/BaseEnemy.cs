using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public abstract class BaseEnemy : BaseSceneObject, IDamageable
    {
        public bool IsAlive
        {
            get { return health > 0; }
        }
        
        [SerializeField]
        protected float health = 100f;
        
        public float CurrentHealth => health;
        
        public void ApplyDamage(float damage, Vector3 damageDirection)
        {    
            if (!IsAlive) Die();
        }

        public void Die()
        {
            Collider.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}