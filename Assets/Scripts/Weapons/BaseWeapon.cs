using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public abstract class BaseWeapon : BaseSceneObject
    {
        [SerializeField]
        protected BaseAmmo bulletPrefab;

        [SerializeField] 
        protected int countBullets;

        [SerializeField]
        protected float shootForce;

        [SerializeField]
        protected float reloadTime;

        [SerializeField]
        protected float timeout;

        protected float lastShotTime;
        protected int countBulletsInWeapon = 0;

        public virtual void Fire()
        {
            if (Time.time - lastShotTime < timeout) return;
            lastShotTime = Time.time;

            FireAction();
        }

        protected abstract void FireAction();
        public abstract void Reload();

    }
}