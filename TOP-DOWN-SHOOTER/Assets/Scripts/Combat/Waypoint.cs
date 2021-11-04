
using UnityEngine;

namespace TopDownShooter.Combat
{
    public class Waypoint : MonoBehaviour
    {
        public Transform[] wayPoints;
        public int GetNextPointIndex(int currentIndex){
            if (currentIndex == wayPoints.Length -1) return 0;
            return currentIndex+1;
        }
        public Transform GetPositionFromIndex(int index){
            if (index > wayPoints.Length -1 || index < 0) return null;
            return wayPoints[index];
        }
        private void OnDrawGizmos() {
            for (int i = 0; i < wayPoints.Length ; i++)
            {
                Gizmos.DrawSphere(wayPoints[i].position,0.5f);
                Gizmos.DrawLine(wayPoints[i].position,wayPoints[GetNextPointIndex(i)].position);
            }
        }
    }

}