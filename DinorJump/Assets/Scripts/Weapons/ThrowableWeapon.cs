using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEditor.Tilemaps;
using UnityEngine;
using WeaponSystem;

public class ThrowableWeapon : MonoBehaviour
{
    Vector2 startPosition = Vector2.zero;
    RangeWeaponData data;
    Vector2 direction;
    bool isInitalized = false;
    Rigidbody2D Rb2d;

    public Transform spriteTransform;

    public float rotationSpeed = 1;

    [Header("Collision detection Data")]
    [SerializeField]
    private Vector2 center = Vector2.zero;

    [SerializeField]
    [Range(0.1f, 2f)]
    private float radius = 1;
    [SerializeField]
    private Color GizmosColor = Color.red;
    private LayerMask layerMask;

    private void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        if(spriteTransform == null)
        {
            spriteTransform = transform.GetChild(0);
        }
    }
    private void Start()
    {
       startPosition = transform.position;
    }

    public void Initialized(RangeWeaponData data, Vector2 direction, LayerMask mask )
    {
        this.direction = direction;
        this.data = data;
        isInitalized = true;
        Rb2d.velocity = direction * data.weaponThrowSpeed;
        layerMask = mask;
    }
    private void Update()
    {
        if(isInitalized)
        {
            Fly();
            DetectionCollision();
            if (Vector2.Distance(startPosition, transform.position) >= data.attackRange)
            {
                Destroy(gameObject);
            }
        }
    }

    private void DetectionCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (hit)
        {
            foreach (var hittable in hit.GetComponents<IHittable>())
            {
                hittable.GetHit(gameObject, data.weaponDamage);
            }
            Destroy(gameObject);
        }
    }

    private void Fly()
    {
        spriteTransform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * rotationSpeed * -direction.x);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawSphere(transform.position + (Vector3)center, radius);
    }
}
