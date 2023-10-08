using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : MonoBehaviour
{
    public int param;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}