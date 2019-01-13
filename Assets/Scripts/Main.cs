using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace FPS
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        public FlashlightController FlashlightController { get; private set; }
        public InputController InputController { get; private set; }
        public BoxController BoxController { get; private set; }
        public WeaponsController WeaponsController { get; private set; }
        public TeammateController TeammateController { get; private set; }
        
        public Text DamageUI { get; private set; }

        private void Awake()
        {
            if (Instance) DestroyImmediate(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            InputController = gameObject.AddComponent<InputController>();
            FlashlightController = gameObject.AddComponent<FlashlightController>();
            BoxController = gameObject.AddComponent<BoxController>();
            WeaponsController = gameObject.AddComponent<WeaponsController>();
            DamageUI = GameObject.Find("Damage").GetComponent<Text>();
            TeammateController = gameObject.AddComponent<TeammateController>();
        }
    }
}