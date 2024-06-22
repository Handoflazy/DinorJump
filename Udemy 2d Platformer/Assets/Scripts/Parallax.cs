using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Camera _camera;
    private Vector2 startPos;
    [SerializeField,Range(-1,1)]
    private float moveSpeed = 0.5f;
    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Start()
    {
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        Vector2 travel =(Vector2)_camera.transform.position - startPos;
        transform.position = new Vector2((startPos +travel*moveSpeed).x,transform.position.y);
    }
}
