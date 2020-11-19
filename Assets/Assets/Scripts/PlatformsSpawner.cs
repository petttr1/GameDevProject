using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlatformsSpawner : MonoBehaviour
    {
        public Vector3 RespawnPoint;
        [SerializeField] public GameObject player;
        public int AmountSpawned = 5;

        public int MaxEnemyPlatforms = 5;
        public int MaxRewardPlatforms = 1;

        [Range(0f, 1f)] public float RewardPlatformProba = 0.01f;
        [Range(0f, 1f)] public float EnemyPlatformProba = 0.3f;


        public GameObject EnemyPlatform;
        public GameObject RewardPlatform;
        public GameObject BasicPlatform;

        private int EnemyPlatforms;
        private int RewardPlatforms;
        private int PlatformsSpawned;
        private GameObject[] platforms;
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
            player = GameObject.FindGameObjectWithTag("Player");
            // if the platform was not visited yet
            if (active_platform.GetComponent<PlatformManager>().visited == false)
            {
                // refill the players' lightness
                player.GetComponent<Lightness>().RefillLightness(100);
                //deal damage to all other platforms
                platforms = GameObject.FindGameObjectsWithTag("Platform");

                foreach (GameObject platform in platforms)
                {
                    if (platform != active_platform.transform.parent.gameObject)
                    {
                        platform.GetComponentInChildren<PlatformManager>().DealDamage();
                    }
                }
                // spawn new platforms
                active_platform.GetComponent<PlatformManager>().VisitThisPlatform();
                SpawnNewPlatforms(active_platform);
            }
            // set our respawn point
            RespawnPoint = active_platform.transform.position;
            RespawnPoint.y += 5;
        }

        private void SpawnNewPlatforms(GameObject paltform)
        {
            PlatformsSpawned = 0;
            PlatformManager manager = paltform.GetComponent<PlatformManager>();
            EnemyPlatforms = GameObject.FindGameObjectsWithTag("Enemy").Length;
            RewardPlatforms = GameObject.FindGameObjectsWithTag("Reward").Length;

            // if the enemy cap is not reached
            for (int i = MaxEnemyPlatforms - EnemyPlatforms; i >= 0; i--)
            {
                // spawn enemies with the probability of 30%
                if (PlatformsSpawned <= AmountSpawned && EnemyPlatforms < MaxEnemyPlatforms && Random.Range(0f, 1f) <= EnemyPlatformProba)
                {
                    manager.SpawnPlatformOfType(EnemyPlatform, paltform.transform.position);
                    EnemyPlatforms++;
                    PlatformsSpawned++;
                }
            }

            // if the reward cap is not reached
            for (int i = MaxRewardPlatforms - RewardPlatforms; i >= 0; i--)
            {
                // spawn rewards with the probability of 1%
                if (PlatformsSpawned <= AmountSpawned && EnemyPlatforms < MaxEnemyPlatforms && Random.Range(0f, 1f) <= RewardPlatformProba)
                {
                    manager.SpawnPlatformOfType(RewardPlatform, paltform.transform.position);
                    RewardPlatforms++;
                    PlatformsSpawned++;
                }
            }

            // fill in the remaining spawn slots with basic platforms
            for (int i = AmountSpawned - PlatformsSpawned; i >= 0; i--)
            {
                manager.SpawnPlatformOfType(BasicPlatform, paltform.transform.position);
            }

        }
    }
}