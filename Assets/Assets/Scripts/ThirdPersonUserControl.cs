using System;
using UnityEngine;

// !!! THIS CODE IS FROM STANDARD UNITY ASSETS. CUSTOM FUNCTIONS WILL BE MARKED BY A COMMENT !!! //
// MOST OF THE TIME, ALREADY EXISTING FUNCTIONS HAVE BEEN UPDATED OR MODIFIED //
namespace Platform
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        private bool Dash;
        private bool IsDashing;
        private int DashCount;
        Animator m_Animator;
        private void Start()
        {
            m_Cam = Camera.main.transform;
            m_Character = GetComponent<ThirdPersonCharacter>();
            m_Animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }
            // if the max dash cap is not reached, player can dash
            if (DashCount < gameObject.GetComponent<PowerUps>().GetMaxDashCount())
            {
                Dash = Input.GetMouseButtonDown(1);
            }
            if (Input.GetMouseButtonUp(1) && gameObject.GetComponent<PowerUps>().IsDashing())
            {
                gameObject.GetComponent<PowerUps>().InterruptDash();
                Dash = !Dash;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RespawnPlayer();
            }
            // if the palyer is on the ground, reset Dash Count
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                DashCount = 0;
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v*m_CamForward + h*m_Cam.right;

            // && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded")
            if (Dash)
            {
                gameObject.GetComponent<PowerUps>().Dash();
                Dash = false;
                DashCount++;
            }

            if (m_Move.magnitude > 1f) m_Move.Normalize();

            // pass all parameters to the character control script
            IsDashing = gameObject.GetComponent<PowerUps>().IsDashing();
            m_Character.Move(m_Move, m_Jump, IsDashing);
            m_Jump = false;
        }

        private void RespawnPlayer()
        {
            var gameControl = GameObject.FindGameObjectWithTag("GameController");
            transform.position = gameControl.GetComponent<PlatformsSpawner>().RespawnPoint;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }

        public void EndDash()
        {
            Dash = false;
        }
    }
}
