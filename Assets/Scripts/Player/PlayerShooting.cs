﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
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
        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponentInChildren<LineRenderer>();
            rend.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GamePauseControl.GamePaused && Input.GetButtonDown("Fire1"))
            {
                audioSource.PlayOneShot(LaserShotSounds, audioSource.volume);
                ShootingPoint = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
                rend.SetPosition(0, ShootiongOrigin.position);
                if (Physics.Raycast(ShootingPoint, cam.transform.forward, out hit, Range))
                {
                    rend.SetPosition(1, hit.point);
                    Instantiate(HitParticles, hit.point, Quaternion.LookRotation(Vector3.forward, hit.normal));
                    GameObject objectHit = hit.transform.gameObject;
                    if (objectHit.CompareTag("Enemy"))
                    {
                        objectHit.GetComponentInChildren<Lightness>().DealDamage(Damage);
                    }
                }
                else
                {
                    rend.SetPosition(1, cam.transform.forward * cam.farClipPlane);
                }
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