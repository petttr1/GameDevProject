﻿using System.Collections;
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
                    player_rigidbody.velocity = originalVelocity;
                    dash = false;
                }
                else
                {
                    player_rigidbody.AddForce(transform.forward * dashPower);
                }
            }
        }

        public void Dash(Vector3 move)
        {
            currentDashTime = 0;
            originalVelocity = player_rigidbody.velocity;
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
