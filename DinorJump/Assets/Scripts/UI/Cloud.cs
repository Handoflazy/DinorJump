using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    private float mineScale = 1f, maxScale = 1.5f;
    public float speed = 70;
    public event Action OnOutSideScreen;
    public float outSideScreenDistance;

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
        if(Vector2.Distance(transform.parent.position, transform.position) > outSideScreenDistance)
        {
            OnOutSideScreen?.Invoke();
            Destroy(gameObject);
        }
    }
    public float GetCloudScale()
    {
        return Random.Range(mineScale, maxScale);
    }
    public void Initialize(float distance, Action onOutsideScreenHandler)
    {
       outSideScreenDistance = distance;
        OnOutSideScreen += onOutsideScreenHandler;
    }
}
