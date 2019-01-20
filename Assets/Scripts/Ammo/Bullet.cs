using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FPS
{
    public class Bullet : BaseAmmo
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float lifetime = 2f;
        
        private float speed;
        private bool isHitted = false;
        private ParticleSystem collParticle;

        [SerializeField]
        private string poolID = "Bullet01";
        public override string PoolID
        {
            get { return poolID; }
        }
        [SerializeField]
        private int objectsCount = 30;
        public override int ObjectsCount
        {
            get { return objectsCount; }
        }

        private void Awake()
        {
            collParticle = GetComponentInChildren<ParticleSystem>();
        }

        public override void Initialize(float force, Transform firepoint)
        {
            transform.position = firepoint.position;
            transform.rotation = firepoint.rotation;
            
            CancelInvoke();
            isHitted = false;
            speed = force;
            Invoke("Disable", lifetime);    
            
            gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            if (isHitted)
            {
                collParticle.Play();
                return;
            }
            var finalPos = transform.position + transform.forward * speed * Time.fixedDeltaTime;
            RaycastHit hit;
            if (Physics.Linecast(transform.position, finalPos, out hit, layerMask))
            {
                isHitted = true;
                transform.position = hit.point;

                IDamageable box = hit.collider.GetComponent<IDamageable>();
                if (box != null) box.ApplyDamage(damage, transform.forward * speed);
                
                Invoke("Disable", 0.3f);
            }
            else
            {
                transform.position = finalPos;
            }
        }
    }
}