using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class PlayerModel : MonoBehaviour
    {
        public static PlayerModel LocalPlayer { get; private set; }
        public BaseWeapon[] Weapons;
        public Image[] UI;
        
        private void Awake()
        {
            if (LocalPlayer) DestroyImmediate(gameObject);
            else LocalPlayer = this;
        }
    }
}