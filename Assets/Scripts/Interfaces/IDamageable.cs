using UnityEngine;

namespace FPS
{
    public interface IDamageable
    {
        float CurrentHealth { get; }
        void ApplyDamage(float damage, Vector3 damageDirection);
    }
}