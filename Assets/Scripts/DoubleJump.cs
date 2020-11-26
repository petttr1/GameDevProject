using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class DoubleJump : MonoBehaviour
    {
        private Rigidbody player_rigidbody;
        public bool IsEnabled = true;
        public bool doubleJumping;
        void Start()
        {
            player_rigidbody = gameObject.GetComponent<Rigidbody>();
            enabled = IsEnabled;
        }
        public void AddPowerUp()
        {
            enabled = true;
        }

        public void DoDoubleJump(Vector3 originalVelo, float forwardPower, float upPower)
        {
            doubleJumping = true;            
            player_rigidbody.velocity = originalVelo;
            player_rigidbody.velocity = new Vector3(player_rigidbody.velocity.x * forwardPower, upPower, player_rigidbody.velocity.z * forwardPower);
        }
    }
}