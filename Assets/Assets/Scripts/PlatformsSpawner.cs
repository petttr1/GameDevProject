using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlatformsSpawner : MonoBehaviour
    {
        public Vector3 RespawnPoint;

        // Start is called before the first frame update
        void Start()
        {
            GameEvents.current.onPlayerPlatformLand += onPlayerLand;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void onPlayerLand(GameObject active_platform)
        {
            // if the platform was not visited yet
            if (active_platform.GetComponent<PlatformManager>().visited == false)
            {

                //deal damage to all other platforms
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

                foreach (GameObject platform in platforms)
                {
                    if (platform != active_platform.transform.parent.gameObject)
                    {
                        platform.GetComponentInChildren<PlatformManager>().DealDamage();
                    }
                }
                // spawn new platforms
                active_platform.GetComponent<PlatformManager>().VisitThisPlatform(active_platform, gameObject);
            }
            // set our respawn point
            RespawnPoint = active_platform.transform.position;
            RespawnPoint.y += 5;
        }
    }
}