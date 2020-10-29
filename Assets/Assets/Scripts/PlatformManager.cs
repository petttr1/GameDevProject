using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlatformManager : MonoBehaviour
    {
        public int hitPoints = 5;
        public bool visited = false;
        public GameObject[] platformChoices;
        private MaterialPropertyBlock propBlock;
        private Renderer rend;
        // Start is called before the first frame update
        void Start()
        {
            propBlock = new MaterialPropertyBlock();
            rend = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            // if the platform has 0 hp, despawn it
            if (hitPoints <= 0)
            {
                Destroy(GetComponent<BoxCollider>());
                Destroy(gameObject);
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
                SpawnNewPlatforms(platform, player);
                // refill players' lightness
                player.GetComponent<Lightness>().RefillLightness(100);
                // mark it as visited
                visited = true;
            }
        }
        private void SpawnNewPlatforms(GameObject platform, GameObject player)
        {
            Vector3 pos = transform.position;
            Vector3 player_orientation = player.transform.forward.normalized;
            Vector3 next_pos = pos + player_orientation * 35;
            int choice_index = Random.Range(0, platformChoices.Length);
            GameObject paltform_choice = platformChoices[choice_index];
            GameObject next_platform1 = Instantiate(paltform_choice, next_pos, Quaternion.identity) as GameObject;
            next_platform1.GetComponent<PlatformManager>().platformChoices = platformChoices;
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
