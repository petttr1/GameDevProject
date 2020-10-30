﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
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

        private void onPlayerLand(GameObject active_platform)
        {
            if (active_platform.GetComponentInChildren<PlatformManager>().visited == false)
            {

                //deal damage to old platforms
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

                foreach (GameObject platform in platforms)
                {
                    platform.GetComponentInChildren<PlatformManager>().DealDamage();
                }
                // spawn new platforms
                active_platform.GetComponentInChildren<PlatformManager>().VisitThisPlatform(active_platform, gameObject);
            }
        }


    }
}