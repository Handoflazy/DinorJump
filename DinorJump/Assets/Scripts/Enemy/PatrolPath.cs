using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace SVS.AI
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> patrolPoints = new List<Transform>();

        public int Length { get { return patrolPoints.Count; } }

        [Header("Gizmo Color:")]

        public Color pointColor = Color.blue;
        public float pointSize = 1;

        public Color lineColor = Color.magenta;
        public bool ShowGizmo;

        public struct PathPoint
        {
            public int Index;
            public Vector2 position;
        }
        public PathPoint GetClosestPathPoint(Vector3 agentPosition)
        {
            var result = patrolPoints.Select((point, index) => new { Index = index, Position = (Vector2)point.position, Distance = Vector2.Distance(agentPosition, point.position) })
                .Aggregate((p1, p2) => p1.Distance < p2.Distance ? p1 : p2);
            return new PathPoint { Index = result.Index, position = result.Position };

        }

        public PathPoint GetNextPathPoint(int index)
        {
            var newIndex = index + 1 >= patrolPoints.Count ? 0 : index + 1;
            return new PathPoint { Index = newIndex, position = patrolPoints[newIndex].position };
        }

        private void OnDrawGizmos()
        {
            if (ShowGizmo)
            {
                if(patrolPoints.Count > 0)
                {
                    for(int i = 0; i < patrolPoints.Count; i++)
                    {
                        if (patrolPoints[i] == null)
                        {
                            return;
                        }
                        Gizmos.color = pointColor;
                        Gizmos.DrawSphere(patrolPoints[i].position, pointSize);
                        if(patrolPoints.Count==1&&i==0)
                        {
                            return;
                        }
                        Gizmos.color = lineColor;
                        if (i == 0)
                            continue;
                        Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i-1].position);
                        if(patrolPoints.Count>2&&i == patrolPoints.Count - 1)
                        {
                            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
                        }
                    }
                }
            }
        }
    }
}