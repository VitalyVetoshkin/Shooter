using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class BoxController : BaseController
    {
        private bool boxIsCapture = false;
        private BoxModel box;
        private RaycastHit hit;
        private Ray ray;
        private Rigidbody rb;
        private Vector3 zero = new Vector3(0, 0, 0);

        public void SwitchBox()
        {
            if (boxIsCapture) QuitBox();
            else CaptureBox();
        }

        private void CaptureBox()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit) || hit.distance > 3f) return;
            if (hit.transform.GetComponent<BoxModel>())
            {
                box = hit.transform.GetComponent<BoxModel>();
                rb = box.GetComponent<Rigidbody>();
                rb.useGravity = false;
                box.transform.parent = Camera.main.transform;
                StartCoroutine(ControlBox());
                boxIsCapture = true;
            }                     
        }

        private void QuitBox()
        {
            StopCoroutine(ControlBox());
            rb.useGravity = true;
            box.transform.parent = null;
            box = null;         
            boxIsCapture = false;
        }

        private IEnumerator ControlBox()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                if (boxIsCapture)
                {
                    rb.velocity = zero;
                    rb.angularVelocity = zero;
                }
            }
        }
    }
}