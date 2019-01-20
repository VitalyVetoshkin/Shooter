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
        
        private void Awake()
        {
            fireParticle = GetComponentInChildren<ParticleSystem>();
            anim = GetComponent<Animator>();
            countBulletsInWeapon = countBullets;                 
        }

        protected override void FireAction()
        {
            if (countBulletsInWeapon <= 0) return;
            
            BaseAmmo bullet = ObjectsPool.Instance.GetObject(ammoID) as BaseAmmo;
            
            bullet.Initialize(shootForce, firepoint);

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
                
                if (textCountBulletsUI)
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