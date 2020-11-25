using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlayerShooting : MonoBehaviour
    {
        public Camera cam;
        public float Damage;
        private Vector3 ShootingPoint;
        RaycastHit hit;
        LineRenderer rend;
        public Transform ShootiongOrigin;
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
            if (Input.GetButtonDown("Fire1"))
            {
                ShootingPoint = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
                StartCoroutine(ShotVisuals());
                rend.SetPosition(0, ShootiongOrigin.position);
                if (Physics.Raycast(ShootingPoint, cam.transform.forward, out hit, Mathf.Infinity))
                {
                    rend.SetPosition(1, hit.point);
                    GameObject objectHit = hit.transform.gameObject;
                    if (objectHit.tag =="Enemy")
                    {
                        objectHit.GetComponentInChildren<Lightness>().DealDamage(Damage);
                    }
                }
                else
                {
                    rend.SetPosition(1, cam.transform.forward * cam.farClipPlane);
                }
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