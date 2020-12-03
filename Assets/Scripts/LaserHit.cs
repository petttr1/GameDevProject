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
            // PlayLaserHit();
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

        /*private IEnumerator PlayLaserHit()
        {
            laserHit.Play();
            yield return new WaitForSeconds(TTL);
            laserHit.Stop();
            DestroyParticles();
        }

        private void DestroyParticles()
        {
            Destroy(gameObject);
        }*/
    }
}
