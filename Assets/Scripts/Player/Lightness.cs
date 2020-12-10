﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(ScoreAdding))]
    public class Lightness : MonoBehaviour
    {
        public float lightness = 100;
        public float fadingRate = 5;
        public bool enemy;
        public int AmountScoreAdded = 100;
        //whether to subtract lightness every tick
        public bool fading = false;
        private MaterialPropertyBlock propBlock;
        private Renderer rend;

        // Start is called before the first frame update
        void Start()
        {
            propBlock = new MaterialPropertyBlock();
            rend = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            if (rend == null)
            {
                rend = gameObject.GetComponentInChildren<MeshRenderer>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (lightness > 0)
            {
                if (fading)
                {
                    lightness -= Time.deltaTime * fadingRate;
                    lightness = Mathf.Clamp(lightness, 0.0f, 100.0f);
                }
                UpdateLightnessVisual();
            }
            else
            {
                if (enemy)
                {
                    EnemyDeath(gameObject);
                }
                else
                {
                    GameEvents.current.PlayerDeath();
                }
            }
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

        public void DealDamage(float amount)
        {
            lightness -= amount;
        }

        public void EnemyDeath(GameObject enemy)
        {
            ScoreAdding.AddScoreToPlayer(AmountScoreAdded);
            Destroy(gameObject);
        }
    }
}