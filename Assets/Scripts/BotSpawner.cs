using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class BotSpawner : MonoBehaviour
    {
        public Waypoint[] Waypoints;
        public EnemyBot BotPrefab;

        private EnemyBot instance;
        
        public float SearchDistance;
        public float AttackDistance;

        public float MaxRandomWPRadius = 20f;


        private void Update()
        {
            if (instance) return;

            instance = Instantiate(BotPrefab, transform.position, transform.rotation);
            instance.Initialize(this);
        }
    }
}