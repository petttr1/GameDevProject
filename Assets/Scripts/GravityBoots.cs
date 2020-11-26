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

        void Start()
        {
            player_rigidbody = gameObject.GetComponent<Rigidbody>();
            enabled = IsEnabled;
            GameEvents.current.onPlayerPlatformLand += PlayerLanded;
        }
        public void AddPowerUp()
        {
            enabled = true;
        }

        private void Update()
        {
            // the gravity is applied when the player presses left shift,
            // until the button is released. Then, it is disabled. Enables again on land.
            if (enabled
                && canUseBoots
                && Input.GetButtonDown("Fire3"))
            {
                Debug.Log("Gravity");
                player_rigidbody.AddForce(Vector3.down * addedGravityForce);
            }
            if (Input.GetButtonUp("Fire3"))
            {
                Debug.Log("Stop Gravity");
                canUseBoots = false;
            }
        }

        private void PlayerLanded(GameObject p)
        {
            Debug.Log("Gravity land");
            canUseBoots = true;
        } 
    }
}