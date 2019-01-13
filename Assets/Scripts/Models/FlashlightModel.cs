using System;
using System.Collections;
using UnityEngine;

namespace FPS
{
    public class FlashlightModel : MonoBehaviour
    {
        public event Action<bool> FlashlightStateChanged;
        public event Action<float> FillAmountChanged;

        public bool IsOn { get { return light.enabled; } }

        [SerializeField] private float chargeUpdateTime = 0.5f;
        [SerializeField] private float rechargeTime = 5f;
        [SerializeField] private float drainMult = 3f;

        private Light light;
        private float fillAmount;

        private void Awake()
        {
            light = GetComponent<Light>();
            fillAmount = 1f;
        }

        private void OnEnable()
        {
            StartCoroutine(ChangeFill());
        }

        private void OnDisable()
        {
            StopCoroutine(ChangeFill());
        }

        public void On()
        {
            if (fillAmount < 0.3f) return;

            light.enabled = true;
            if (FlashlightStateChanged != null) FlashlightStateChanged.Invoke(true);
        }

        public void Off()
        {
            light.enabled = false;
            if (FlashlightStateChanged != null) FlashlightStateChanged.Invoke(false);
        }

        private IEnumerator ChangeFill()
        {
            while (true)
            {
                yield return new WaitForSeconds(chargeUpdateTime);

                if (IsOn)
                {
                    fillAmount = Mathf.Clamp01(fillAmount - (1f / Mathf.Max(0.001f, rechargeTime * drainMult) ) * chargeUpdateTime);
                    if (fillAmount <= 0.001f) Off();
                }
                else
                {
                    fillAmount = Mathf.Clamp01(fillAmount + (1f / Mathf.Max(0.001f, rechargeTime)) * chargeUpdateTime);
                }

                FillAmountChanged?.Invoke(fillAmount);
            }
        }
    }
}