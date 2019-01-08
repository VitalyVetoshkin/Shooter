using UnityEngine;

namespace FPS
{
    public class SecondEnemy : BaseSceneObject, IDamageable
    {
        private GameObject cube;
        private Rigidbody rigidbody;
        private void Awake()
        {
            cube = GetComponentInChildren<MeshFilter>().gameObject;
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }

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
            
            cube.SetActive(cube.activeSelf == false);

            rigidbody.AddForce(damageDirection, ForceMode.Impulse);
            
            if (!IsAlive) Die();
        }

        private void Die()
        {          
            Collider.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}