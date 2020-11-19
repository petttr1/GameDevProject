using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreservePlayerOnReload : MonoBehaviour
{
    static PreservePlayerOnReload Instance;

    private void Awake()
    {
        if (Instance != null) { 
            Destroy(gameObject); 
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}
