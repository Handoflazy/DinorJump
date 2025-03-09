using UnityEngine;

namespace SVS.Platform
{
    public class PlatformColliderAdjuster : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D platformCollider;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            if (!platformCollider)
            {
                platformCollider = GetComponent<BoxCollider2D>();
            }
            if (!spriteRenderer)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }
        private void Start()
        {
            platformCollider.size = new Vector2(spriteRenderer.size.x,platformCollider.size.y);
            platformCollider.offset = new Vector2(0, spriteRenderer.size.y / 2f - platformCollider.size.y / 2f);
        }
        //private void OnDrawGizmos()
        //{
        //    if (spriteRenderer == null)
        //        return;

        //    Gizmos.color = Color.red;
        //    Gizmos.DrawCube(spriteRenderer.bounds.center, spriteRenderer.bounds.size);
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawSphere(spriteRenderer.bounds.center,1);
        //}
    }
}