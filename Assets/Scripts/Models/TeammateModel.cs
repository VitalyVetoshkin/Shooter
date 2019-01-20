using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace FPS
{
    public class TeammateModel : MonoBehaviour
    {
        private NavMeshAgent agent;
        private ThirdPersonCharacter character;

        private Queue<Vector3> navPoints = new Queue<Vector3>();

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;
        }

        private void Update()
        {
            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else 
                if (navPoints != null && navPoints.Count > 0) SetDistination(navPoints.Dequeue());
            else
                character.Move(Vector3.zero, false, false);
        }

        public void SetDistination(Vector3 pos)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pos, out hit, 20f, -1))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                Debug.Log("Wrong Position!");
            }
        }

        public void AddPoint(Vector3 point)
        {
            navPoints.Enqueue(point);
        }
    }
}