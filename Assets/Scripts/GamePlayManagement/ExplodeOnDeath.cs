using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
    public class ExplodeOnDeath : MonoBehaviour
    {
        public GameObject onDestroyParticles;
        private bool isQuitting = false;
        private bool sceneRealoading = false;
        void OnEnable()
        {
            GameEvents.current.onSceneChanging += OnSceneChange;
        }
        void OnDisable()
        {
            GameEvents.current.onSceneChanging -= OnSceneChange;
        }
        private void OnSceneChange()
        {
            Debug.Log("Scene reload");
            sceneRealoading = true;
        }
        void OnApplicationQuit()
        {
            isQuitting = true;
        }

        private void OnDestroy()
        {
            if (!isQuitting && !sceneRealoading)
            {
                ParticleSystem ps = onDestroyParticles.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule psmain = ps.main;
                MeshRenderer rend = gameObject.GetComponent<MeshRenderer>();
                psmain.startColor = rend.material.color;
                Instantiate(onDestroyParticles, gameObject.transform.position, Quaternion.identity);
            }
        }

    }
}