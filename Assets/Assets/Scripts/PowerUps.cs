using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PowerUps : MonoBehaviour
    {
        Rigidbody player_rigidbody;

        public float dashPower;
        public int maxDashCount = 3;

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
                    gameObject.GetComponent<ThirdPersonUserControl>().EndDash();
                }
                else
                {
                    // apply the dash power
                    // player_rigidbody.AddForce(transform.forward * dashPower);
                    player_rigidbody.velocity = transform.forward.normalized * dashPower;
                }
            }
        }

        public void AddPowerUp(int type) 
        {
            Debug.Log("PowerUp added: " + type);
        }

        public void Dash()
        {
            currentDashTime = 0;
            Vector3 velo = player_rigidbody.velocity;
            originalVelocity = transform.InverseTransformDirection(new Vector3(velo.x, 0, velo.z));
            dash = true;
        }

        public void InterruptDash()
        {
            if (dash)
            {
                currentDashTime = maxDashTime;
            }
        }

        public void GravityBoots(Vector3 move)
        {

        }

        public void DoubleJump(Vector3 move)
        {

        }

        public bool IsDashing()
        {
            return dash;
        }

        public int GetMaxDashCount()
        {
            return maxDashCount;
        }
    }
}
