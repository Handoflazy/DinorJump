using SpriteAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField]
    SpriteAnimator animator;

    private void Awake()
    {
        if(animator == null)
            animator = GetComponent<SpriteAnimator>();
        if(boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play("Destruction").SetOnFrame(3, DisableCollider).SetOnEnd(DestroyGO);
        }
    }

    private void DisableCollider(Frame frame)
    {
        boxCollider.enabled = false;
    }
    private void DestroyGO()
    {
        Destroy(gameObject);
    }


}
