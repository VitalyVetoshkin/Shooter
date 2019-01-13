using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class MagicStaff : BaseWeapon
    {
        [SerializeField] private Transform[] firepoints;
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
            
            bullets = new BaseAmmo[countBullets * firepoints.Length];
            for (var i = 0; i < bullets.Length; i++)
            {
                bullets[i] = Instantiate(bulletPrefab, firepoints[0].position, firepoints[0].rotation);
                bullets[i].gameObject.SetActive(false);
            }
        }

        protected override void FireAction()
        {
            if (countBulletsInWeapon <= 0) return;

            if (firepoints != null)
            {
                foreach (var firepoint in firepoints)
                {
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
                }

                fireParticle.Play();

                anim.SetBool("isFire", true);

                countBulletsInWeapon--;
            }
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