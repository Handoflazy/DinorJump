using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    private float width = 10f, length = 10f;
    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.2f);
    [SerializeField]
    private bool ShowGizmo = true;
    [SerializeField]
    private List<Cloud> cloudPrefabs = new List<Cloud>();
    [SerializeField]
    private float CloudSpeed = 50;
    [SerializeField]
    private float scaleModifier = .5f;
    public Canvas canvas;


    public void Start()
    {
        foreach(Transform item in transform)
        {
            if(item.TryGetComponent(out Cloud cloud)) {
                cloud.Initialize(width / 2 * canvas.scaleFactor + 50, SpawnCloud);
            }
        }
    }
    private void SpawnCloud()
    {
        Vector3 pos = GetRandomPosition();
        Cloud cloud = cloudPrefabs[Random.Range(0, cloudPrefabs.Count)];
        float scale = cloud.GetCloudScale() + scaleModifier;
        GameObject cloudObject = Instantiate(cloud.gameObject);
        RectTransform rectTransform = cloudObject.GetComponent<RectTransform>();
        rectTransform.position = pos;
        rectTransform.localScale = Vector3.one*scale*canvas.scaleFactor;
        Cloud newCloud = cloudObject.GetComponent<Cloud>();
        newCloud.speed = CloudSpeed;
        rectTransform.SetParent(transform);
        newCloud.Initialize(width / 2 * canvas.scaleFactor + 50, SpawnCloud);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(transform.position.x - ((width) / 2) * canvas.scaleFactor, 
            Random.Range(transform.position.y - length / 2 * canvas.scaleFactor, transform.position.y + length / 2 * canvas.scaleFactor), 
            1);
    }
    private void OnDrawGizmos()
    {
        if(ShowGizmo&& canvas)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(transform.position, new Vector2(width, length) * canvas.scaleFactor);
        }
    }
}
