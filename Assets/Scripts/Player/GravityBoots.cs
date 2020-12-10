using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityBoots : MonoBehaviour
    {
        public bool IsEnabled = true;
        public float addedGravityForce = 10f;

        private Rigidbody player_rigidbody;
        private bool canUseBoots = false;
        private Vector3 origVelo;

        void Start()
        {
            player_rigidbody = gameObject.GetComponent<Rigidbody>();
            enabled = IsEnabled;
            GameEvents.current.onPlayerJump += EnableBootsUsage;
        }
        private void OnDestroy()
        {
            GameEvents.current.onPlayerJump -= EnableBootsUsage;
        }

        private void Update()
        {
            // the gravity is applied when the player presses left shift,
            // until the button is released. Then, it is disabled. Enables again on land.
            if (!GamePauseControl.GamePaused && enabled && canUseBoots && Input.GetButtonDown("Fire3"))
            {
                origVelo = player_rigidbody.velocity;
                player_rigidbody.velocity = new Vector3(origVelo.x, -addedGravityForce, origVelo.z);
            }
            if (!GamePauseControl.GamePaused && Input.GetButtonUp("Fire3") && canUseBoots)
            {
                canUseBoots = false;
                player_rigidbody.velocity = origVelo;
            }
        }

        private void EnableBootsUsage(Vector3 v)
        {
            canUseBoots = true;
        } 
    }
}