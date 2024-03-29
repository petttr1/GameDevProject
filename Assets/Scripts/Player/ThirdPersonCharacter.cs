using UnityEngine;

// !!! THIS CODE IS FROM STANDARD UNITY ASSETS.														//
// MOST OF THE TIME, ALREADY EXISTING FUNCTIONS HAVE OFTEN BEEN UPDATED OR MODIFIED					//
// ALL OF THE ORIGINAL CODE IS MARKED AND IS IN THE END OF THE FILE
// I AM USING THIS CODE MOSTLY FOR THE ANIMATOR														//
namespace Platform
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Jump))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float InAirMovementImpact = 1f;
		[Range(1f, 4f)] public float m_GravityMultiplier = 2f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;

		void HandleAirborneMovement(Vector3 move, bool dash)
		{
			// HEAVILY MODIFIED. CONTRARY TO THE ORIGINAL FUNCTION, WE WANT TO ALLOW IN AIR PLAYER MOVEMENT
			if (!dash) {
				Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
				m_Rigidbody.AddForce(extraGravityForce);
			}
			m_Rigidbody.AddForce(move * InAirMovementImpact);
			m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
			// handle falling out of the world here
			if (CheckFallingOutOfTheWorld())
            {
				GameEvents.current.PlayerDeath();
			}
		}

		bool CheckFallingOutOfTheWorld()
        {
			// platforms spawn only in the range <-10, 10>, so falling below this value is not wanted.
			// we give the player some grace period in which to use OneUp.
			return m_Rigidbody.position.y <= -15f;
        }

		// modified
		void HandleGroundedMovement(bool jump)
		{
			if (jump)
			{
				m_IsGrounded = false;
				m_Animator.applyRootMotion = false;
				m_GroundCheckDistance = 0.1f;
			}
		}

		//***************CODE BELOW IS FROM STANDARD ASSETS AND NOT (Or only slightly) MODIFIED***************//
		// THIS FUNCTION IS FROM THE ORIGINAL CLASS
		public void Move(Vector3 move, bool jump, bool dash)
		{
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			// Global direction is transformed to local direction of the player
			move = transform.InverseTransformDirection(move);
			// The script determines whether the player stands on the ground or not
			CheckGroundStatus();
			// the move vector is projected on the:
			// - normal of the ground if the palyer stands on the ground
			// - (0, 1, 0) if the player does not stand on the ground
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			// turn and forward stuff - used by animations? 
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;
			ApplyExtraTurnRotation();

			// control and velocity handling is different when grounded and airborne:
			if (m_IsGrounded)
			{
				// basically just handles the jump
				HandleGroundedMovement(jump);
			}
			else
			{
				// adds extra gravity to the fall = simualtion of free fall
				HandleAirborneMovement(move, dash);
			}
			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}
		// THIS FUNCTION IS FROM THE ORIGINAL CLASS
		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			m_OrigGroundCheckDistance = m_GroundCheckDistance;
		}
		// THIS FUNCTION IS FROM THE ORIGINAL CLASS
		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}
		// THIS FUNCTION IS FROM THE ORIGINAL CLASS
		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
			m_Animator.SetBool("OnGround", m_IsGrounded);
			if (!m_IsGrounded)
			{
				m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
			}

			// calculate which leg is behind, so as to leave that leg trailing in the jump animation
			// (This code is reliant on the specific run cycle offset in our animations,
			// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
			float runCycle =
				Mathf.Repeat(
					m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
			float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
			if (m_IsGrounded)
			{
				m_Animator.SetFloat("JumpLeg", jumpLeg);
			}

			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			if (m_IsGrounded && move.magnitude > 0)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
			}
			else
			{
				// don't use that while airborne
				m_Animator.speed = 1;
			}
		}
		// THIS FUNCTION IS FROM THE ORIGINAL CLASS
		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (m_IsGrounded && Time.deltaTime > 0)
			{
				Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}
		// THIS FUNCTION IS FROM THE ORIGINAL CLASS
		void CheckGroundStatus()
		{
            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out RaycastHit hitInfo, m_GroundCheckDistance))
            {
                m_IsGrounded = true;
                m_GroundNormal = hitInfo.normal;
                m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
                m_Animator.applyRootMotion = false;
            }
        }
	}
}
