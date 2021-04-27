using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Dash power up. When used, moves player ion the direction they face with a high speed.
    [RequireComponent(typeof(Rigidbody))]
    public class Dash : MonoBehaviour
    {
        public bool IsEnabled = true;
        public float dashPower = 300;
        public int maxDashCount = 3;
        public AudioSource audioSource;
        public AudioClip DashSound;

        private Rigidbody player_rigidbody;
        private float maxDashTime;
        private bool dash;
        private float currentDashTime;
        private Vector3 originalVelocity;
        private int dashCount;

        void Start()
        {
            player_rigidbody = gameObject.GetComponent<Rigidbody>();
            enabled = IsEnabled;
            currentDashTime = 0;
            dashCount = 0;
            maxDashTime = 0.1f;
            GameEvents.current.onPlayerPlatformLand += PlayerLanded;
            GameEvents.current.onPlayerJump += ResetDashCount;
        }
        private void OnDestroy()
        {
            GameEvents.current.onPlayerPlatformLand -= PlayerLanded;
            GameEvents.current.onPlayerJump -= ResetDashCount;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(1) && dashCount < maxDashCount)
            {
                dashCount++;
                DoDash();
            }
            if (Input.GetMouseButtonUp(1) && dash)
            {
                InterruptDash();
            }
            if (dash)
            {
                currentDashTime += Time.deltaTime;
                if (currentDashTime >= maxDashTime)
                {
                    // restore the original velocity in the new direction the player faces - we dont want the player to go flying into infinity and beyond
                    player_rigidbody.velocity = transform.TransformDirection(transform.InverseTransformDirection(transform.forward) * originalVelocity.magnitude);
                    dash = false;
                }
                else
                {
                    // apply the dash power
                    player_rigidbody.velocity = transform.forward.normalized * dashPower;
                }
            }
        }

        public void DoDash()
        {
            audioSource.PlayOneShot(DashSound, audioSource.volume);
            currentDashTime = 0;
            Vector3 velo = player_rigidbody.velocity;
            // store the original velocity
            originalVelocity = transform.InverseTransformDirection(new Vector3(velo.x, 0, velo.z));
            dash = true;
        }

        public void InterruptDash()
        {
            if (dash)
            {
                currentDashTime = maxDashTime;
            }
        }

        public bool IsDashing()
        {
            return dash;
        }

        public int GetMaxDashCount()
        {
            return maxDashCount;
        }

        private void PlayerLanded(GameObject p, Vector3 d)
        {
            dash = false;
            dashCount = maxDashCount;
        }

        private void ResetDashCount(Vector3 v)
        {
            dashCount = 0;
        }
    }
}