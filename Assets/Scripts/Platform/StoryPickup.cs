using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(ScoreAdding))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(ComponentDestroyer))]
    public class StoryPickup : MonoBehaviour
    {
        public int AmountScoreAdded = 1000;
        public GameObject platformToSpawn;
        public AudioClip PickupSound;

        private GameObject MyParent;
        // Start is called before the first frame update
        void Start()
        {
            MyParent = transform.parent.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            // play pickup sounds
            AudioSource.PlayClipAtPoint(PickupSound, gameObject.transform.position);
            // add score to player
            ScoreAdding.AddScoreToPlayer(AmountScoreAdded);
            // spawn next sotry platform
            SpawnNewStoryPlatform();
            // set thius paltform to despawn
            MyParent.GetComponentInChildren<PlatformManager>().Despawning = true;
            // destroy the pickup
            GetComponent<ComponentDestroyer>().DestroyComponent(gameObject);
        }

        private void SpawnNewStoryPlatform()
        {
            GameObject next_platform = MyParent.GetComponentInChildren<PlatformManager>().SpawnPlatformOfType(platformToSpawn, MyParent.transform.position, Vector3.forward);
            GameEvents.current.NewStoryPlatform(next_platform.transform);
        }
    }
}