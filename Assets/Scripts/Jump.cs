using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(DoubleJump))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] float m_JumpPower = 12f;
        [SerializeField] float JumpForwardPower = 1.5f;

        private Rigidbody j_Rigidbody;
        private Vector3 startingVelocity;
        private bool jumping = false;
        private DoubleJump DoubleJumpComponent;
        // Start is called before the first frame update
        void Start()
        {
            j_Rigidbody = GetComponent<Rigidbody>();
            DoubleJumpComponent = GetComponent<DoubleJump>();
            GameEvents.current.onPlayerPlatformLand += JumpEnded;
        }

        public void DoJump(ref bool grounded, ref Animator anim, ref float GCD)
        {
            // if the jump is called when the player is on the ground
            if (!jumping)
            {
                // store the velocity of the body right before jumping
                // we use this as a starting point of the second jump (if double jumping)
                // in order to avoid multiplying the velocities and floying too far
                startingVelocity = j_Rigidbody.velocity;
                GameEvents.current.PlayerJump(startingVelocity);
                // add velocity to the rigidbody - the actual jump
                j_Rigidbody.velocity = new Vector3(j_Rigidbody.velocity.x * JumpForwardPower, m_JumpPower, j_Rigidbody.velocity.z * JumpForwardPower);
                jumping = true;
                grounded = false;
                anim.applyRootMotion = false;
                GCD = 0.1f;
            }
/*            // else we are double jumping
            else if (!DoubleJumpComponent.doubleJumping)
            {
                DoubleJumpComponent.DoDoubleJump(startingVelocity, JumpForwardPower, m_JumpPower);
            }*/
        }
        private void JumpEnded(GameObject player)
        {
            jumping = false;
        }
    }
}
