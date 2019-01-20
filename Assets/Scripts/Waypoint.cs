using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPS
{
    public class Waypoint : MonoBehaviour
    {
        public float WaitTime = 1f;
        public UnityEvent OnReachEvent;
    }
}