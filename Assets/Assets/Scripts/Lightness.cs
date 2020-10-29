using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class Lightness : MonoBehaviour
    {
        public float lightness = 100;

        private MaterialPropertyBlock propBlock;
        private Renderer rend;

        // Start is called before the first frame update
        void Start()
        {
            propBlock = new MaterialPropertyBlock();
            rend = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (lightness >= 0)
            {
                lightness -= 0.1f;
                lightness = Mathf.Clamp(lightness, 0.0f, 100.0f);
            }
            // else Death() = TODO
            UpdateLightnessVisual();
        }

        void UpdateLightnessVisual()
        {
            rend.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_Lightness", lightness);
            rend.SetPropertyBlock(propBlock);
        }

        public void RefillLightness(float amount)
        {
            lightness += amount;
        }
    }
}