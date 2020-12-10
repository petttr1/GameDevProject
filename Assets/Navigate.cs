using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(AudioSource))]
    public class Navigate : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public float TTL = 2f;
        private Vector3 direction;
        private float TTLeft;
        private bool navigating = false;

        private void Start()
        {
            TTLeft = TTL;
        }

        private void Update()
        {
            if (navigating && TTLeft > 0)
            {
                transform.position += direction * moveSpeed * Time.deltaTime;
                TTLeft -= Time.deltaTime;
            }
            else if (TTLeft <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void setDirection(Vector3 dest)
        {
            direction = (dest - transform.position).normalized;
            transform.LookAt(dest);
            navigating = true;
        }
    }
}