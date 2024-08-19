using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using UnityEngine;

public class DestroyUtil : MonoBehaviour
{
    public void DestroySelf()
    {
        DestroyObject(gameObject);
        StopAllCoroutines();
    }
    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    } 
    public void DestroySelfAfterTime(int t)
    {
        Invoke(nameof(DestroySelf) ,t);
    }
}
