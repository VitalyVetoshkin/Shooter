using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class SingleBarreledFirearm : BaseWeapon
    {
        [SerializeField] private Transform firepoint;
        private ParticleSystem fireParticle;
        private Animator anim;
        [SerializeField] private Text textCountBulletsUI;
        [SerializeField] private Image ui;
        
        private BaseAmmo[] bullets;
        
        private void Awake()
        {
            fireParticle = GetComponentInChildren<ParticleSystem>();
            anim = GetComponent<Animator>();
            countBulletsInWeapon = countBullets;     
            
            bullets = new BaseAmmo[countBullets];
            for (var i = 0; i < bullets.Length; i++)
            {
                bullets[i] = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                bullets[i].gameObject.SetActive(false);
            }
        }

        protected override void FireAction()
        {
            if (countBulletsInWeapon <= 0) return;

            if (bullets != null)
            {
                BaseAmmo bullet = bullets[0];
                
                for (var i = 0; i < bullets.Length; i++)
                {
                    if (bullets[i].gameObject.activeSelf == false)
                    {
                        bullet = bullets[i];
                        bullet.gameObject.SetActive(true);
                        bullet.transform.position = firepoint.position;
                        bullet.transform.forward = firepoint.forward;
                        break;
                    }                                
                }           
                bullet.Initialize(shootForce);
            }

            fireParticle.Play();
            
            anim.SetBool("isFire", true);

            countBulletsInWeapon--;
        }

        protected void StopFire()
        {
            anim.SetBool("isFire", false);
        }

        public override void Reload()
        {
            anim.SetBool("isReload", true);
        }

        protected void StopReload()
        {
            anim.SetBool("isReload", false);
            countBulletsInWeapon = countBullets;
        }

        private IEnumerator StateBullets()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                            
                textCountBulletsUI.text = countBulletsInWeapon.ToString();
            }          
        }

        private void OnEnable()
        {
            StartCoroutine(StateBullets());
        }

        private void OnDisable()
        {
            StopCoroutine(StateBullets());
        }

    }
}