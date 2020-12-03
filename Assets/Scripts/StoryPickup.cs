using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
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
            SpawnNewStoryPlatform();
            MyParent.GetComponentInChildren<PlatformManager>().Despawning = true;
            Destroy(gameObject);
        }

        private void SpawnNewStoryPlatform()
        {
            GameObject next_platform = MyParent.GetComponentInChildren<PlatformManager>().SpawnPlatformOfType(platformToSpawn, MyParent.transform.position);
            GameEvents.current.NewStoryPlatform(next_platform.transform);
        }
    }
}