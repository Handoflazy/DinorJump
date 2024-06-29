using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int health = 3;
    void Start()
    {
        DinorSingleton.Instance.PlayerUI.InitializeMaxHealth(health);
        DinorSingleton.Instance.PlayerUI.SetPoints(0);
    }

}
