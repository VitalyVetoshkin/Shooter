using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPS
{
    public class EnemyBot : MonoBehaviour, IDamageable
    {
        public Transform EyesTransform;
        
        private float searchDistance;
        private float attackDistance;

        private float maxRandomWPRadius;
        private Waypoint[] waypoints;

        private bool useRandomWP;

        private int currentWP;
        private float currentWPTimeout;
        private Transform targetTransform;
        private NavMeshAgent agent;
        private Vector3 randomPos;

        private BaseWeapon weapon;

        [SerializeField]
        private float health = 20;
        
        private bool IsAlive
        {
            get { return health > 0; }
        }

        private Animator anim;

        public void Initialize(BotSpawner spawner)
        {
            searchDistance = spawner.SearchDistance;
            attackDistance = spawner.AttackDistance;
            maxRandomWPRadius = spawner.MaxRandomWPRadius;
            waypoints = spawner.Waypoints;
            useRandomWP = waypoints == null || waypoints.Length < 2;
            
            Main.Instance.EnemyBotsController.AddBot(this);
        }
        
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            weapon = GetComponentInChildren<BaseWeapon>();
            anim = GetComponent<Animator>();
        }

        public void SetTarget(Transform target)
        {
            targetTransform = target;
        }

        private void Update()
        {
            var seenTarget = false;
            if (targetTransform)
            {
                var dist = Vector3.Distance(transform.position, targetTransform.position);
                if (dist < attackDistance)
                {
                    seenTarget = IsTargetSeen();
                    if (seenTarget)
                    {
                        agent.SetDestination(targetTransform.position);
                        weapon.Fire();
                        anim.SetBool("isFire", true);
                        if (weapon.CountBulletsInWeapon <= 0) weapon.Reload();
                    } 
                    else 
                        anim.SetBool("isFire", false);
                }
                else if (dist < searchDistance)
                {
                    seenTarget = IsTargetSeen();
                    if (seenTarget) agent.SetDestination(targetTransform.position);
                }
            }
            
            if (seenTarget) return;

            if (useRandomWP)
            {
                agent.SetDestination(randomPos);
                if (!agent.hasPath || agent.remainingDistance > maxRandomWPRadius * 2)
                    randomPos = GenerateRandomWaypoint();
            }
            else
            {
                if (waypoints == null || waypoints.Length < 2)
                {
                    useRandomWP = true;
                }
                else
                {
                    agent.SetDestination(waypoints[currentWP].transform.position);
                    if (!agent.hasPath)
                    {
                        currentWPTimeout += Time.deltaTime;
                        if (currentWPTimeout >= waypoints[currentWP].WaitTime)
                        {
                            currentWPTimeout = 0;
                            currentWP++;
                            if (currentWP >= waypoints.Length) currentWP = 0;
                        }
                    }
                }
            }
            
            if (agent.velocity.x > 0.1f || agent.velocity.y > 0.1f) 
                anim.SetBool("isWalk", true);
            else 
                anim.SetBool("isWalk", false);
        }

        private Vector3 GenerateRandomWaypoint()
        {
            var result = maxRandomWPRadius * Random.insideUnitSphere;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(transform.position + result, out hit, maxRandomWPRadius * 1.5f,
                NavMesh.AllAreas))
                return hit.position;
            else
                return transform.position;           
        }

        private bool IsTargetSeen()
        {
            RaycastHit hit;
            if (Physics.Linecast(EyesTransform.position, targetTransform.position, out hit) && 
                hit.transform == targetTransform)
            {
                Debug.DrawLine(EyesTransform.position, hit.point, Color.red);
                return true;
            }
            Debug.DrawLine(EyesTransform.position, hit.point, Color.green);
            return false;
        }

        public float CurrentHealth => health;
        public void ApplyDamage(float damage, Vector3 damageDirection)
        {
            if (!IsAlive) return;
            health -= damage;
            if (!IsAlive) Die();
        }

        public void Die()
        {
            Main.Instance.EnemyBotsController.RemoveBot(this);
            
            Destroy(gameObject);
        }
    }
}
