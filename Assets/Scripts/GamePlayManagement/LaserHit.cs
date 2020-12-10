using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class LaserHit : MonoBehaviour
    {
        public float TTL = 0.5f;
        private float remainnigDur;
        private ParticleSystem laserHit;

        void Start()
        {
            remainnigDur = TTL;
            laserHit = gameObject.GetComponent<ParticleSystem>();
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
