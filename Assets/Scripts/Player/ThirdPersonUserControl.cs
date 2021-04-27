using System;
using UnityEngine;

// THIS CLASS STARTED AS STANDARD ASSET BUT HAS NOTHING IN COMMON NOW. //
// EVERYTHING WAS CHANGED. //
namespace Platform
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    [RequireComponent(typeof(Dash))]
    [RequireComponent(typeof(Rigidbody))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        private bool IsDashing;
        private Dash DashComponent;
        private void Start()
        {
            m_Cam = Camera.main.transform;
            m_Character = GetComponent<ThirdPersonCharacter>();
            DashComponent = gameObject.GetComponent<Dash>();
            GameEvents.current.onPlayerJump += PlayerJumped;
        }
        private void OnDestroy()
        {
            GameEvents.current.onPlayerJump -= PlayerJumped;
        }
        private void Update()
        {
            if (!GamePauseControl.GamePaused)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    RespawnPlayer();
                }
            }
        }
        private void FixedUpdate()
        {
            // read inputs
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v*m_CamForward + h*m_Cam.right;

            if (m_Move.magnitude > 1f) m_Move.Normalize();

            // pass all parameters to the character control script
            IsDashing = DashComponent.IsDashing();
            m_Character.Move(m_Move, m_Jump, IsDashing);
            m_Jump = false;
        }

        private void RespawnPlayer()
        {
            var gameControl = GameObject.FindGameObjectWithTag("GameController");
            transform.position = gameControl.GetComponent<PlatformsSpawner>().RespawnPoint;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
        private void PlayerJumped(Vector3 velo)
        {
            m_Jump = true;
        }
    }
}
