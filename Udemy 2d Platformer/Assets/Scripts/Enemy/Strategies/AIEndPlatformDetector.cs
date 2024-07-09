using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace SVS.AI
{
    public class AIEndPlatformDetector : MonoBehaviour
    {
        public BoxCollider2D detectorCollider;
        public LayerMask groundMask;
        public float groundRaycastLenth = -.1f;
        [Range(0, 1)]
        public float groundRaycastDelay = 0.1f;

        public bool PathBlocked { get; private set; }


        public event Action OnPathBlocked;

        [Space(10)]

        public Color colliderCollor = Color.magenta;
        public Color groundRaycastColor = Color.blue;
        public bool ShowGizmoz = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((groundMask & (1 << collision.gameObject.layer))!=0)
              OnPathBlocked?.Invoke();
        }
        private void Start()
        {
            StartCoroutine(CheckGroundCoroutine());
        }

        IEnumerator CheckGroundCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(groundRaycastDelay);
                var hit = Physics2D.Raycast(detectorCollider.bounds.center, Vector2.down, groundRaycastLenth, groundMask);
                if (hit.collider==null)
                {
                    OnPathBlocked?.Invoke();
                }
                PathBlocked = hit.collider == null;
            }
        }
        private void OnDrawGizmos()
        {
            if(ShowGizmoz && detectorCollider !=null)
            {
                Gizmos.color = groundRaycastColor;
                Gizmos.DrawRay(detectorCollider.bounds.center,Vector2.down * groundRaycastLenth);
                Gizmos.color = colliderCollor;
                Gizmos.DrawCube(detectorCollider.bounds.center,detectorCollider.bounds.size);
            }

        }
    }
}