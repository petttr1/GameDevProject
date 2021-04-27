using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Shooting of the enemy. When activated, the enemy starts to aim. After a delay it shoots.
    [RequireComponent(typeof(LineRenderer))]
    public class EnemyShooting : MonoBehaviour
    {
        public float Damage;
        public Transform ShootingOrigin;
        public float ShootingRange = 20f;
        public float AimTime = 3f;

        public Material AimMat;
        public Material ShootMat;
        public GameObject HitParticles;

        public AudioSource audioSource;
        public AudioClip LaserShotSounds;

        private float TimeTaken;
        private RaycastHit hit;
        private LineRenderer rend;
        readonly private WaitForSeconds ShootingDuration = new WaitForSeconds(.2f);
        private bool TakingAim = false;

        void Start()
        {
            rend = GetComponentInChildren<LineRenderer>();
            rend.enabled = false;
        }
        void Update()
        {
            if (TakingAim)
            {
                TimeTaken += Time.deltaTime;
                if (TimeTaken >= AimTime)
                {
                    // If ready to shoot, shoot.
                    Shoot();
                }
                else
                {
                    // if not, keep showing the orange, aiming beam.
                    rend.SetPosition(0, transform.position);
                    rend.SetPosition(1, transform.TransformPoint(transform.InverseTransformDirection(transform.forward) * ShootingRange));
                }
            }
        }
        public void StopAiming()
        {
            TakingAim = false;
            rend.enabled = false;
        }
        public void StartAiming()
        {
            TakingAim = true;
            TimeTaken = 0f;
            rend.enabled = true;
            rend.material = AimMat;
        }
        private IEnumerator ShotVisuals()
        {
            // for a small amount of time changes the color of the beam from orange to red = shot.
            rend.material = ShootMat;
            yield return ShootingDuration;
            rend.material = AimMat;
        }
        private void Shoot()
        {
            // plat shot audio
            audioSource.PlayOneShot(LaserShotSounds, audioSource.volume);
            // show shoot visuals
            StartCoroutine(ShotVisuals());
            rend.SetPosition(0, transform.position);
            // try to hit
            if (Physics.Raycast(transform.position, transform.forward, out hit, ShootingRange))
            {
                rend.SetPosition(1, hit.point);
                GameObject objectHit = hit.transform.gameObject;
                // spawn particles on hit.
                Instantiate(HitParticles, hit.point, Quaternion.LookRotation(Vector3.forward, hit.normal));
                if (objectHit.GetComponentInChildren<Lightness>() != null)
                {
                    // if an object with lightness is hit, deal damage
                    objectHit.GetComponentInChildren<Lightness>().DealDamage(Damage);
                }
            }
            else
            {
                rend.SetPosition(1, transform.TransformPoint(transform.InverseTransformDirection(transform.forward) * ShootingRange));
            }
            TimeTaken = 0f;
        }
    }
}