using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PowerUps : MonoBehaviour
    {
        Rigidbody player_rigidbody;

        public float dashPower;

        private float maxDashTime;
        private bool dash;
        private float currentDashTime;
        private Vector3 originalVelocity;
        // Start is called before the first frame update
        void Start()
        {
            player_rigidbody = gameObject.GetComponent<Rigidbody>();
            currentDashTime = 0;
            maxDashTime = 0.1f;
        }

        // Update is called once per frame
        void Update()
        {
            if (dash)
            {
                currentDashTime += Time.deltaTime;
                if (currentDashTime >= maxDashTime)
                {
                    // restore the original velocity - we dont want the player to go flying into infinity and beyond
                    player_rigidbody.velocity = transform.TransformDirection(originalVelocity);
                    dash = false;
                }
                else
                {
                    // apply negative gravity force - we want the dash to be exactly straight
                    Vector3 extraGravityForce = (Physics.gravity * GetComponent<ThirdPersonCharacter>().m_GravityMultiplier) - Physics.gravity;
                    player_rigidbody.AddForce(-extraGravityForce);

                    // apply the dash power
                    player_rigidbody.AddForce(transform.forward * dashPower);
                }
            }
        }

        public void Dash(Vector3 move)
        {
            currentDashTime = 0;
            originalVelocity = transform.InverseTransformDirection(player_rigidbody.velocity);
            dash = true;
        }

        public void GravityBoots(Vector3 move)
        {

        }

        public void DoubleJump(Vector3 move)
        {

        }
    }
}
