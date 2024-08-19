using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateUtil : MonoBehaviour
{
    public GameObject instantiatePrefab;

    public void CreateObject()
    {
    

        Instantiate(instantiatePrefab,transform.position,Quaternion.identity);
    }
}
