using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{  
    public abstract class BaseAmmo : BaseSceneObject, IPoolable
    {
        [SerializeField] protected float damage = 20f;
        public abstract string PoolID { get; }
        public abstract int ObjectsCount { get; }
        
        public abstract void Initialize(float force, Transform firepoint);

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}