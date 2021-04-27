using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Used with enemies - the orange beans. When player enters their trigger, start to aim at them. Change color to red and start to aim and shoot.
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(EnemyShooting))]
    public class RegisterPlayer : MonoBehaviour
    {
        public bool Active = false;
        public AudioSource audioSource;
        public AudioClip ActiveSound;
        public AudioClip IdleSound;

        private MaterialPropertyBlock propBlock;
        private Renderer rend;
        private GameObject player;
        private Color originalColor;

        void Start()
        {
            propBlock = new MaterialPropertyBlock();
            rend = GetComponent<Renderer>();
        }
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
            // play angry sound
            audioSource.clip = ActiveSound;
            audioSource.Play();
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
            // play not angry sound
            audioSource.clip = IdleSound;
            audioSource.Play();
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