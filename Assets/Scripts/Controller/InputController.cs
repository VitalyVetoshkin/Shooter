using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class InputController : BaseController
    {
        private void Update()
        {
            if (Input.GetButtonDown("SwitchFlashlight"))
                Main.Instance.FlashlightController.SwitchFlashlight();
            
            if (Input.GetButtonUp("SwitchBox"))
                Main.Instance.BoxController.SwitchBox();
            
            if (Input.GetButtonDown("ChangeWeapon"))
                Main.Instance.WeaponsController.ChangeWeapon();
            
            if (Input.GetButtonDown("Fire1"))
                Main.Instance.WeaponsController.Fire();

            if (Input.GetButtonDown("Reload"))
                Main.Instance.WeaponsController.Reload();
            
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                Main.Instance.WeaponsController.ChangeWeapon();

            if (Input.GetButtonDown("TeammateCommand"))
                Main.Instance.TeammateController.MoveCommand();
        }
    }
}