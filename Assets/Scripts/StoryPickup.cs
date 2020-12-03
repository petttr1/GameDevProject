using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(ScoreAdding))]
    public class StoryPickup : MonoBehaviour
    {
        public GameObject platformToSpawn;
        private GameObject MyParent;
        // Start is called before the first frame update
        void Start()
        {
            MyParent = transform.parent.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            // add score to player
            GetComponent<ScoreAdding>().AddScoreToPlayer();
            // spawn next sotry platform
            SpawnNewStoryPlatform();
            // set thius paltform to despawn
            MyParent.GetComponentInChildren<PlatformManager>().Despawning = true;
            // destroy the pickup
            Destroy(gameObject);
        }

        private void SpawnNewStoryPlatform()
        {
            GameObject next_platform = MyParent.GetComponentInChildren<PlatformManager>().SpawnPlatformOfType(platformToSpawn, MyParent.transform.position);
            GameEvents.current.NewStoryPlatform(next_platform.transform);
        }
    }
}