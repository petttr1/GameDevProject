using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsSpawner : MonoBehaviour
{

    public int amount = 10;
    public bool isGameStart = true;
    public Vector3 center;
    public GameObject platform;
    // Start is called before the first frame update
    void Start()
    {
        if (isGameStart == true){
            isGameStart = false;
            Instantiate(platform, center, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
