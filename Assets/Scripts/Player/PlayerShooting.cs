using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Manages shooting for player. When (LMB) is pressed, transforms the middle point of screen to coords, raycasts and reposrts a hit.
    public class PlayerShooting : MonoBehaviour
    {
        public Camera cam;
        public float Damage = 50f;
        public float Range = 30f;
        public Transform ShootiongOrigin;
        public GameObject HitParticles;
        public AudioSource audioSource;
        public AudioClip LaserShotSounds;

        private Vector3 ShootingPoint;
        RaycastHit hit;
        LineRenderer rend;
        private WaitForSeconds ShootingDuration = new WaitForSeconds(.1f);

        void Start()
        {
            rend = GetComponentInChildren<LineRenderer>();
            rend.enabled = false;
        }
        void Update()
        {
            if (!GamePauseControl.GamePaused && Input.GetButtonDown("Fire1"))
            {
                // play shooting audio
                audioSource.PlayOneShot(LaserShotSounds, audioSource.volume);
                // we want to shoot where the crosshair is - middle of the screen
                ShootingPoint = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
                // set the initial point of our laser
                rend.SetPosition(0, ShootiongOrigin.position);
                // raycast
                if (Physics.Raycast(ShootingPoint, cam.transform.forward, out hit, Range))
                {
                    GameObject objectHit = hit.transform.gameObject;
                    // if the hit is enemy, deal damage
                    if (objectHit.CompareTag("Enemy"))
                    {
                        // if hit, draw the line between player and hit
                        rend.SetPosition(1, hit.point);
                        // hit particles go off at the point of hit
                        Instantiate(HitParticles, hit.point, Quaternion.LookRotation(Vector3.forward, hit.normal));
                        objectHit.GetComponentInChildren<Lightness>().DealDamage(Damage);
                    }
                    else
                    {
                        // else just draw the line in the dir. we are looking
                        rend.SetPosition(1, transform.TransformPoint(transform.InverseTransformDirection(cam.transform.forward) * Range));
                    }
                }
                else
                {
                    // else just draw the line in the dir. we are looking
                    rend.SetPosition(1, transform.TransformPoint(transform.InverseTransformDirection(cam.transform.forward) * Range));
                }
                // play the laser visuals - draw the line
                StartCoroutine(ShotVisuals());
            }
        }

        private IEnumerator ShotVisuals()
        {
            rend.enabled = true;
            yield return ShootingDuration;
            rend.enabled = false;
        }
    }
}