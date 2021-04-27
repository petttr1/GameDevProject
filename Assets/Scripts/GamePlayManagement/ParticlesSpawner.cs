using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Spawns particles for a TTL seconds and destroys them.
    public class ParticlesSpawner : MonoBehaviour
    {
        public float TTL = 0.5f;

        private float remainnigDur;

        void Start()
        {
            remainnigDur = TTL;
        }

        private void Update()
        {
            if (remainnigDur > 0)
            {
                remainnigDur -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
