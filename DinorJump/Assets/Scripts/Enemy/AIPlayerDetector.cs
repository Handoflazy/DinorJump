using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AI
{
    public class AIPlayerDetector : MonoBehaviour
    {
        // Start is called before the first frame update
        [field: SerializeField]
        public bool PlayerDetected { get; private set; }
        public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.transform.position;
        [SerializeField]
        private Transform detectorOrigin;
        public Vector2 detectorSize = Vector2.one;
        public Vector2 detectorOriginOffset = Vector2.zero;

        public float detectionDelay = 0.3f;
        public LayerMask detectorLayerMask;

        private GameObject target;

        public GameObject Target { get => target; private set { target = value; PlayerDetected = target != null; } }

        [Header("Gizmo Parameter")]
        public Color gizmodIdleColor = Color.green;
        public Color gizmoDetectedColor = Color.red;

        public bool showGizmo = true;

        void Start()
        {
            StartCoroutine(DetectionCoroutine());
        }

        IEnumerator DetectionCoroutine()
        {
            yield return new WaitForSeconds(detectionDelay);
            Collider2D hit = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
            if (hit)
            {
                Target = hit.gameObject;
            }
            else
            {
                Target = null;
            }
            StartCoroutine(DetectionCoroutine());
        }

        private void OnDrawGizmos()
        {
            if (showGizmo && detectorOrigin != null)
            {
                Gizmos.color = gizmodIdleColor;
                if (PlayerDetected)
                {
                    Gizmos.color = gizmoDetectedColor;
                }
                Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
            }
        }
    }
}