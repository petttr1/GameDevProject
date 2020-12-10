using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class ComponentDestroyer : MonoBehaviour
    {
        public GameObject onDestroyParticles;

        // Start is called before the first frame update
        public void DestroyComponent(GameObject component)
        {
            ParticleSystem ps = onDestroyParticles.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            MeshRenderer rend = component.GetComponent<MeshRenderer>();
            if (rend == null)
            {
                rend = component.GetComponentInChildren<MeshRenderer>();
            }
            psmain.startColor = rend.material.color;
            Instantiate(onDestroyParticles, component.transform.position, Quaternion.identity);
            Debug.Log($"Despawning {component.name}");
            Destroy(component);
        }
    }
}
