using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(ScoreAdding))]
    [RequireComponent(typeof(AudioSource))]
    public class PowerUpPickup : MonoBehaviour
    {
        public int AmountScoreAdded = 500;
        public AudioClip PickupSound;

        private void OnTriggerEnter(Collider other)
        {
            // play pickup sounds
            AudioSource.PlayClipAtPoint(PickupSound, gameObject.transform.position);
            // audioSource.PlayOneShot(PickupSound, audioSource.volume);
            // For now this is a Score Boost
            ScoreAdding.AddScoreToPlayer(AmountScoreAdded);
            Destroy(gameObject);
        }
    }
}
