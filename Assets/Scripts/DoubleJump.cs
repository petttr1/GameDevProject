using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(Rigidbody))]
    public class DoubleJump : MonoBehaviour
    {
        public bool IsEnabled = true;
        public float forwardPower = 1.5f;
        public float upPower = 12f;
        private Rigidbody player_rigidbody;
        private bool canDoubleJump;
        private Vector3 originalVelocity;
        void Start()
        {
            player_rigidbody = gameObject.GetComponent<Rigidbody>();
            enabled = IsEnabled;
            GameEvents.current.onPlayerPlatformLand += DoubleJumpEnded;
            GameEvents.current.onPlayerJump += FirstJumpStarted;
        }
        public void AddPowerUp()
        {
            enabled = true;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump") && canDoubleJump)
            {
                DoDoubleJump();
            }
        }
        public void DoDoubleJump()
        {
            canDoubleJump = false;
            player_rigidbody.velocity = transform.TransformDirection(transform.InverseTransformDirection(transform.forward) * originalVelocity.magnitude);
            player_rigidbody.velocity = new Vector3(player_rigidbody.velocity.x * forwardPower, upPower, player_rigidbody.velocity.z * forwardPower);
        }

        private void DoubleJumpEnded(GameObject p)
        {
            canDoubleJump = false;
        }

        private void FirstJumpStarted(Vector3 velo)
        {
            originalVelocity = velo;
            canDoubleJump = true;
        }
    }
}