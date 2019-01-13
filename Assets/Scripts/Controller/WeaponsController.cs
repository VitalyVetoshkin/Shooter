using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class WeaponsController : BaseController
    {
        private BaseWeapon[] weapons;
        private int currentWeapon;
        private Image[] UI;
        
        private void Awake()
        {
            weapons = PlayerModel.LocalPlayer.Weapons;
            UI = PlayerModel.LocalPlayer.UI;

            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].IsVisible = i == currentWeapon;
                UI[i].gameObject.SetActive(i == currentWeapon);
            }
        }

        public void ChangeWeapon()
        {
            weapons[currentWeapon].IsVisible = false;
            UI[currentWeapon].gameObject.SetActive(false);
            currentWeapon++;
            if (currentWeapon >= weapons.Length) currentWeapon = 0;
            weapons[currentWeapon].IsVisible = true;
            UI[currentWeapon].gameObject.SetActive(true);
        }

        public void Fire()
        {
            if (weapons != null && weapons.Length > currentWeapon 
                                && weapons[currentWeapon] != null)
            {
                weapons[currentWeapon].Fire();
            }
        }

        public void Reload()
        {
            if (weapons != null && weapons.Length > currentWeapon 
                                && weapons[currentWeapon] != null)
            {
                weapons[currentWeapon].Reload();
            }
        }
    }
}