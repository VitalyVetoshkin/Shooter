using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    [RequireComponent(typeof(Text))]
    public class FlashlightChargeView : MonoBehaviour
    {
        private Text fillText;
        private FlashlightModel model;
        private int fillValue;

        [SerializeField] private Color ChargeFull;
        [SerializeField] private Color ChargeMedium;
        [SerializeField] private Color ChargeLow;

        private void Awake()
        {
            fillText = GetComponent<Text>();
            model = FindObjectOfType<FlashlightModel>();

            model.FillAmountChanged += OnFillAmountChanged;

            OnFillAmountChanged(1f);
        }

        private void OnFillAmountChanged(float fillAmount)
        {        
            int.TryParse(string.Join("", fillText.text.Where(c => char.IsDigit(c))), out fillValue);
            if (Mathf.Abs(fillAmount - fillValue) < 0.02f) return;
            fillText.text = $"{Mathf.CeilToInt(fillAmount*100)}%";

            if (fillAmount > 0.7f && fillText.color != ChargeFull) fillText.color = ChargeFull;
            if (fillAmount > 0.3f && fillAmount < 0.7f && fillText.color != ChargeMedium) fillText.color = ChargeMedium;
            if (fillAmount < 0.3f && fillText.color != ChargeLow) fillText.color = ChargeLow;
        }

        private void OnDestroy()
        {
            model.FillAmountChanged -= OnFillAmountChanged;
        }
    }
}
