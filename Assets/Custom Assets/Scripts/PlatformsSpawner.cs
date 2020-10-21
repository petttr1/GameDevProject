using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsSpawner : MonoBehaviour
{

    public int amount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPlayerPlatformLand += onPlayerLand;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void onPlayerLand(GameObject active_platform) {
        active_platform.GetComponent<PlatformManager>().visitThisPlatform(active_platform, gameObject);
    }

    
}
