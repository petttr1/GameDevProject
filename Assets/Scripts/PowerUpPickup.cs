using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(ScoreAdding))]
    public class PowerUpPickup : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            // For now this is a Score Boost
            GetComponent<ScoreAdding>().AddScoreToPlayer();
            Destroy(gameObject);
        }
    }
}
