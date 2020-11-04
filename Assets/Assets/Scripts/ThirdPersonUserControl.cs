using System;
using UnityEngine;

// !!! THIS CODE IS FROM STANDARD UNITY ASSETS. CUSTOM FUNCTIONS WILL BE MARKED BY A COMMENT !!! //
// MOST OF THE TIME, ALREADY EXISTING FUNCTIONS HAVE OFTEN BEEN UPDATED OR MODIFIED //
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
        private bool Respawn;

        private void Start()
        {
            m_Cam = Camera.main.transform;
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }
            if (!Dash)
            {
                Dash = Input.GetMouseButtonDown(1);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RespawnPlayer();
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

            if (Dash)
            {
                gameObject.GetComponent<PowerUps>().Dash(m_Move);
            }

            if (m_Move.magnitude > 1f) m_Move.Normalize();

            // pass all parameters to the character control script
            m_Character.Move(m_Move, m_Jump);
            m_Jump = false;
            Dash = false;
        }

        private void RespawnPlayer()
        {
            var gameControl = GameObject.FindGameObjectWithTag("GameController");
            transform.position = gameControl.GetComponent<PlatformsSpawner>().RespawnPoint;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
