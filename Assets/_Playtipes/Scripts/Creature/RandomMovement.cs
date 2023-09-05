using UnityEngine;
using UnityEngine.AI;

namespace Playtipes.Creature {
    [DisallowMultipleComponent]
    #region RequireComponents
    [RequireComponent(typeof(NavMeshAgent))]
    #endregion
    public class RandomMovement : MonoBehaviour {
        private Animator animator;

        private NavMeshAgent agent;

        [SerializeField] private float range;

        [SerializeField] private Transform centrePoint;


        private void Awake() {
            animator = GetComponentInChildren<Animator>();
        }



        private void Start() {
            agent = GetComponent<NavMeshAgent>();

            if (centrePoint == null)
                centrePoint = transform;
        }



        private void Update() {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) {

                    agent.SetDestination(point);
                }
            }

            if (animator == null)
                return;

            float speed = agent.velocity.magnitude / agent.speed;
            animator.SetBool("IsWalking", speed > 0.1f);
        }



        private bool RandomPoint(Vector3 center, float range, out Vector3 result) {

            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }
}
