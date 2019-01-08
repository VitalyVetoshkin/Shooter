using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{  
    public abstract class BaseAmmo : BaseSceneObject
    {
        [SerializeField] protected float damage = 20f;
        public abstract void Initialize(float force);
    }
}