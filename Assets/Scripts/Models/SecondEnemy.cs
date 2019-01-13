using UnityEngine;

namespace FPS
{
    public class SecondEnemy : BaseEnemy, IDamageable
    {
        private GameObject cube;
        private Rigidbody rigidbody;
        private void Awake()
        {
            cube = GetComponentInChildren<MeshFilter>().gameObject;
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }       
        public void ApplyDamage(float damage, Vector3 damageDirection)
        {
            base.ApplyDamage(damage, damageDirection);

            health -= damage;

            Main.Instance.DamageUI.text = damage.ToString();
            
            cube.SetActive(cube.activeSelf == false);

            rigidbody.AddForce(damageDirection, ForceMode.Impulse);           
        }

        public void Die()
        {          
            base.Die();
        }
    }
}