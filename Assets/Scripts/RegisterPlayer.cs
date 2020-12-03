using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(EnemyShooting))]
    public class RegisterPlayer : MonoBehaviour
    {
        public bool Active = false;

        private MaterialPropertyBlock propBlock;
        private Renderer rend;
        private GameObject player;
        private Color originalColor;
        // Start is called before the first frame update
        void Start()
        {
            propBlock = new MaterialPropertyBlock();
            rend = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Active)
            {
                // if the enemy is activated, always turn towards player
                transform.LookAt(player.transform.position + new Vector3(0f, 1f, 0f));
            } 
        }

        private void OnTriggerEnter(Collider other)
        {
            // store self color
            originalColor = rend.material.color;
            // start being active - attacking player
            Active = true;
            player = other.gameObject;
            // change color to - active visuals
            rend.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Vector4(3.0f, 0f, 0f, 1.0f));
            rend.SetPropertyBlock(propBlock);
            // start aiming (and shooting) at player
            GetComponent<EnemyShooting>().StartAiming();
        }

        private void OnTriggerExit(Collider other)
        {
            // deactivate
            Active = false;
            // restore original color
            rend.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", originalColor);
            rend.SetPropertyBlock(propBlock);
            // stop attacking player
            GetComponent<EnemyShooting>().StopAiming();
        }
    }
}