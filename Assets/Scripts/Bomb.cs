using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class Bomb : BaseSceneObject, IDamage
    {
        [SerializeField]
        private float damage;

        public float DamageCount => damage;

        private void OnCollisionEnter(Collision obj)
        {
            //obj.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * damage / 2);
            
            IDamageable dam = obj.gameObject.GetComponent<IDamageable>();
            if (dam != null) dam.ApplyDamage(damage, Vector3.up);
        }
    }
}
