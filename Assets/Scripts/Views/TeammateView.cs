using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class TeammateView : BaseSceneObject
    {
        private TeammateModel model;

        protected override void Awake()
        {
            base.Awake();

            model = GetComponentInParent<TeammateModel>();
            
            TeammateController.TeammateSelected += OnTeammateSelected;
            IsVisible = false;
        }

        private void OnTeammateSelected(TeammateModel teammate)
        {
            IsVisible = model == teammate;
        }

        private void OnDestroy()
        {
            TeammateController.TeammateSelected -= OnTeammateSelected;
        }
    }
}