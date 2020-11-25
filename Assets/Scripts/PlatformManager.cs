using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlatformManager : MonoBehaviour
    {
        public int hitPoints = 5;

        //radii of platform spawning zones
        public float MinRadius;
        public float MaxRadius;
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
                gameObject.GetComponentInParent<PlatformDespawner>().Despawn();
            }
        }

        public void VisitThisPlatform()
        {
            // if the paltform is not yet visited
            if (visited == false)
            {
                // remove its emission
                UpdateVisual();
                // mark it as visited
                visited = true;
            }
        }

        public void SpawnPlatformOfType(GameObject platform, Vector3 center)
        {
            Vector3 next_pos = CalculateNextPosition(platform, center);
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
            // pass along the original platfom choices
            next_platform.GetComponentInChildren<PlatformManager>().platformChoices = platformChoices;
        }

        Vector3 CalculateNextPosition(GameObject next_platform, Vector3 center)
        {
            var nextPlatfromManager = next_platform.GetComponentInChildren<PlatformManager>();
            // get radius derived from the next paltfroms' radii choices
            // this is the distance the platform will be from current platform
            float radius = Random.Range(nextPlatfromManager.MinRadius, nextPlatfromManager.MaxRadius);
            // get an angle (position on a circle)
            float random_angle = Random.Range(0f, 2*Mathf.PI);
            // calculate x and y coordinate on a unit circle and multiply by radius (distance)
            float x_coord = Mathf.Cos(random_angle) * radius;
            float z_coord = Mathf.Sin(random_angle) * radius;
            // change height a bit (not the main goal, but we want variability)
            float y_coord = Random.Range(-3f, 3f);
            // add the new coords to the original (makes the platfrom we landed on the center of a new coordinate system
            // in which we generate the new platform)
            return new Vector3(x_coord, y_coord, z_coord) + center;
        }

        public void UpdateVisual()
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
