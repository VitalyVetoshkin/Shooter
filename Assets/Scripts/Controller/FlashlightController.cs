using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class FlashlightController : BaseController
    {
        private FlashlightModel model;

        private void Awake()
        {
            model = FindObjectOfType<FlashlightModel>();
            model.Off();
        }

        public void SwitchFlashlight()
        {
            if (model.IsOn) model.Off();
            else model.On();
        }
    }
}