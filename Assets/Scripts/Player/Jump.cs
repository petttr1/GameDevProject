using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Jumping of a player.
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(DoubleJump))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] float m_JumpPower = 12f;
        [SerializeField] float JumpForwardPower = 1.5f;

        public AudioSource audioSource;
        public AudioClip JumpSound;

        private Rigidbody j_Rigidbody;
        private Vector3 startingVelocity;
        private bool jumping = false;
        void Start()
        {
            j_Rigidbody = GetComponent<Rigidbody>();
            GameEvents.current.onPlayerPlatformLand += JumpEnded;
        }

        private void OnDestroy()
        {
            GameEvents.current.onPlayerPlatformLand -= JumpEnded;
        }

        private void Update()
        {
            if (!GamePauseControl.GamePaused && Input.GetButtonDown("Jump") && !jumping)
            {
                DoJump();
            }
            if (!GamePauseControl.GamePaused && Input.GetButtonUp("Jump") && !jumping)
            {
                GameEvents.current.PlayerJump(startingVelocity);
                jumping = true;
            }
        }

        private void DoJump()
        {
            // play jump audio
            audioSource.PlayOneShot(JumpSound, audioSource.volume);
            // store the velocity of the body right before jumping
            // we use this as a starting point of the second jump (if double jumping)
            // in order to avoid multiplying the velocities and floying too far
            startingVelocity = j_Rigidbody.velocity;
            // add velocity to the rigidbody - the actual jump
            j_Rigidbody.velocity = new Vector3(j_Rigidbody.velocity.x * JumpForwardPower, m_JumpPower, j_Rigidbody.velocity.z * JumpForwardPower);
        }

        private void JumpEnded(GameObject player, Vector3 dir)
        {
            jumping = false;
        }
    }
}
