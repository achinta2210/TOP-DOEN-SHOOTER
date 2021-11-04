using UnityEngine;
using UnityEngine.AI;
using TopDownShooter.Combat;

namespace TopDownShooter.Control
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour {
        [SerializeField] Waypoint wayPointNetwork;
        [SerializeField] float dwelTime = 3.0f;
        [SerializeField] float distanceTolarance = 0.3f;
        NavMeshAgent navMeshAgent;
        int currentWaypointIndex = 0;
        float timeSinceStartDwelling = 0;
        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            Move(wayPointNetwork.wayPoints[currentWaypointIndex].position);
            SetSpeed(2f);
        }
        private void Update() {
           Petrol();
        }
        
        public void Move(Vector3 target){
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target);
        }
        public void Stop(){
            navMeshAgent.isStopped = true;
        }
        public void Petrol(){
           if (Vector3.Distance(wayPointNetwork.wayPoints[currentWaypointIndex].position,
           transform.position)<distanceTolarance){
                Stop();
                if (timeSinceStartDwelling > dwelTime){
                    currentWaypointIndex = wayPointNetwork.GetNextPointIndex(currentWaypointIndex);
                    Move(wayPointNetwork.wayPoints[currentWaypointIndex].position);
                    timeSinceStartDwelling = 0;
                }else{
                    timeSinceStartDwelling += Time.deltaTime;
                }
                
           }
        }
        public void SetSpeed(float speed){
            navMeshAgent.speed = speed;
        }
    }
}