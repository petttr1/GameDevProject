using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
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

                transform.LookAt(player.transform);
                Debug.DrawRay(transform.position, transform.forward, Color.green);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            originalColor = rend.material.color;
            Active = true;
            player = other.gameObject;
            rend.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Vector4(3.0f, 0f, 0f, 1.0f));
            rend.SetPropertyBlock(propBlock);
        }

        private void OnTriggerExit(Collider other)
        {
            Active = false;
            rend.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", originalColor);
            rend.SetPropertyBlock(propBlock);
        }
    }
}