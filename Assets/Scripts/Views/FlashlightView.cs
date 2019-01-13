using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class FlashlightView : MonoBehaviour
    {
        [SerializeField]
        private Color onColor;
        [SerializeField]
        private Color offColor;

        private FlashlightModel model;
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            model = FindObjectOfType<FlashlightModel>();

            model.FlashlightStateChanged += OnFlashlightStateChanged;
            OnFlashlightStateChanged(false);
        }

        private void OnFlashlightStateChanged(bool state)
        {
            image.color = (state) ? onColor : offColor;
        }

        private void OnDestroy()
        {
            model.FlashlightStateChanged -= OnFlashlightStateChanged;
        }
    }
}