using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(ScoreAdding))]
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(ComponentDestroyer))]
    public class PlatformManager : MonoBehaviour
    {
        public int hitPoints = 5;
        public bool Despawning = true;
        //radii of platform spawning zones
        public float MinRadius;
        public float MaxRadius;
        public bool visited = false;
        public int AmountScoreAdded = 50;

        public AudioSource audioSource;
        public AudioClip PlatformLandSOund;

        private MaterialPropertyBlock propBlock;
        private Renderer rend;
        void Start()
        {
            GameEvents.current.onDealDamagePlatforms += DealDamage;
            propBlock = new MaterialPropertyBlock();
            rend = GetComponent<Renderer>();
        }

        private void OnDestroy()
        {
            GameEvents.current.onDealDamagePlatforms -= DealDamage;
        }

        // Update is called once per frame
        void Update()
        {
            // if the platform has 0 hp, despawn it
            if (hitPoints <= 0)
            {
                gameObject.GetComponentInParent<ComponentDestroyer>().DestroyComponent(gameObject.transform.parent.gameObject);
            }
        }

        public void VisitThisPlatform()
        {
            audioSource.PlayOneShot(PlatformLandSOund, audioSource.volume);
            // if the paltform is not yet visited
            if (visited == false)
            {
                // add socre for visitiong this platform
                ScoreAdding.AddScoreToPlayer(AmountScoreAdded);
                // remove its emission
                UpdateVisual();
                // mark it as visited
                visited = true;
            }
        }

        public GameObject SpawnPlatformOfType(GameObject platform, Vector3 center, Vector3 DirectionAdjust)
        {
            Vector3 next_pos = CalculateNextPosition(platform, center, DirectionAdjust);
            next_pos.y = Mathf.Clamp(next_pos.y, -10f, 10f);
            GameObject next_platform = Instantiate(platform, next_pos, Quaternion.identity);
            // rotate the platform by 0-90 degrees by the vertical axis 
            // (the paltfrom is square, other angles would have no visible effect)
            float yaw = Random.Range(0f, 90f);
            // rotate the platform by (-5)-5 degrees in transverse and longitudal axes.
            // (roll, pitch and yaw add variations to the platforms, the world will seem more diverse)
            float pitch = Random.Range(-5f, 5f);
            float roll = Random.Range(-5f, 5f);
            // rotate the newly spawned platform
            next_platform.transform.eulerAngles = new Vector3(roll, yaw, pitch);
            return next_platform;
        }

        Vector3 CalculateNextPosition(GameObject next_platform, Vector3 center, Vector3 DirectionAdjust)
        {
            var nextPlatfromManager = next_platform.GetComponentInChildren<PlatformManager>();
            // get radius derived from the next platfroms' radii choices
            // this is the distance the platform will be from current platform
            float radius = Random.Range(nextPlatfromManager.MinRadius, nextPlatfromManager.MaxRadius);
            // get an angle (position on a half-circle)
            float random_angle = Random.Range(-90f, 90f);
            // adjst the angle to the direction player is headed in
            var dir = Quaternion.AngleAxis(random_angle, Vector3.up) * DirectionAdjust;
            // calculate coords for the next paltform
            var coords = center + dir * radius;
            // change height a bit (not the main goal, but we want variability)
            coords.y = Random.Range(-3f, 3f);
            return coords;
        }

        public void UpdateVisual()
        {
            rend.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_GlowStrength", 0.0f);
            rend.SetPropertyBlock(propBlock);
        }

        private void DealDamage(GameObject platform)
        {
            // if any other platform was visited, deal 1 damage to all other paltforms
            if (platform != gameObject && Despawning)
                hitPoints--;
        }
    }
}
