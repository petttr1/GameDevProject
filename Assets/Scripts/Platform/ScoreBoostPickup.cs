using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Used on pickups (the magenta balls), plays a nice sound and adds score to the player.
    [RequireComponent(typeof(ScoreAdding))]
    [RequireComponent(typeof(AudioSource))]
    public class ScoreBoostPickup : MonoBehaviour
    {
        public int AmountScoreAdded = 500;
        public AudioClip PickupSound;

        private void OnTriggerEnter(Collider other)
        {
            // play pickup sounds
            AudioSource.PlayClipAtPoint(PickupSound, gameObject.transform.position);
            // For now this is a Score Boost
            ScoreAdding.AddScoreToPlayer(AmountScoreAdded);
            Destroy(gameObject);
        }
    }
}
