using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlatformManager : MonoBehaviour
    {
        public int hitPoints = 5;
        public int AmountSpawned = 1;

        //radii of platform spawning zones
        public float r1 = 50;
        public float r2 = 100;
        public float r3 = 150;
        public bool visited = false;
        public GameObject[] platformChoices;
        private MaterialPropertyBlock propBlock;
        private Renderer rend;
        private float[] radii;
        // Start is called before the first frame update
        void Start()
        {
            propBlock = new MaterialPropertyBlock();
            rend = GetComponent<Renderer>();
            radii = new float[3];
            radii[0] = r1;
            radii[1] = r2;
            radii[2] = r3;
        }

        // Update is called once per frame
        void Update()
        {
            // if the platform has 0 hp, despawn it
            if (hitPoints <= 0)
            {
                gameObject.GetComponentInParent<PlatformDespawner>().Despawn();
            }
        }

        public void VisitThisPlatform(GameObject platform, GameObject player)
        {
            // iof the paltform is not yet visited
            if (visited == false)
            {
                // remove its emission
                UpdateVisual();
                // spawn new paltforms (around this one)
                SpawnNewPlatforms(platform);
                // refill players' lightness
                player.GetComponent<Lightness>().RefillLightness(100);
                // mark it as visited
                visited = true;
            }
        }
        private void SpawnNewPlatforms(GameObject platform)
        {
            for (int i = 0; i < AmountSpawned; i++)
            {
                Vector3 next_pos = CalculateNextPosition(platform);
                int choice_index = Random.Range(0, platformChoices.Length);
                GameObject paltform_choice = platformChoices[choice_index];
                GameObject next_platform = Instantiate(paltform_choice, next_pos, Quaternion.identity) as GameObject;
                next_platform.GetComponentInChildren<PlatformManager>().platformChoices = platformChoices;

            }
        }

        Vector3 CalculateNextPosition(GameObject orig_platform)
        {
            float radius = radii[Random.Range(0, 3)];
            float random_angle = Random.Range(0f, 2*Mathf.PI);
            float x_coord = Mathf.Cos(random_angle) * radius;
            float z_coord = Mathf.Sin(random_angle) * radius;
            return new Vector3(x_coord, orig_platform.transform.position.y, z_coord);
        }

        private void UpdateVisual()
        {
            rend.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_GlowStrength", 0.0f);
            rend.SetPropertyBlock(propBlock);
        }

        public void DealDamage()
        {
            // if any other platform was visited, deal 1 damage to all other paltforms
            hitPoints--;
        }
    }
}
