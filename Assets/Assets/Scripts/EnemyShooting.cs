using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class EnemyShooting : MonoBehaviour
    {
        public float Damage;
        public Transform ShootingOrigin;
        public float ShootingRange = 20f;
        public float AimTime;

        public Material AimMat;
        public Material ShootMat;

        private float TimeTaken;
        private RaycastHit hit;
        private LineRenderer rend;
        private WaitForSeconds ShootingDuration = new WaitForSeconds(.2f);
        private bool TakingAim = false;
        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponentInChildren<LineRenderer>();
            rend.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (TakingAim)
            {
                TimeTaken += Time.deltaTime;
                if (TimeTaken >= AimTime)
                {
                    Shoot();
                }
                else
                {
                    rend.SetPosition(0, transform.position);
                    rend.SetPosition(1, transform.TransformPoint(transform.forward * ShootingRange));
                    // Debug.Log(transform.gameObject.name + ", " + transform.TransformDirection(transform.forward * ShootingRange) + ", " + transform.forward);
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
            rend.material = ShootMat;
            yield return ShootingDuration;
            rend.material = AimMat;
        }

        private void Shoot()
        {
            StartCoroutine(ShotVisuals());
            rend.SetPosition(0, transform.position);
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                rend.SetPosition(1, hit.point);
                GameObject objectHit = hit.transform.gameObject;
                if (objectHit.GetComponentInChildren<Lightness>() != null)
                {
                    objectHit.GetComponentInChildren<Lightness>().DealDamage(Damage);
                }
            }
            else
            {
                rend.SetPosition(1, transform.forward * ShootingRange);
            }
            TimeTaken = 0f;
        }
    }
}