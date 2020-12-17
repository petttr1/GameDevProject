using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
    // Shows particle effects on Destroy of the component.
    [RequireComponent(typeof(MeshRenderer))]
    public class ExplodeOnDeath : MonoBehaviour
    {
        public GameObject onDestroyParticles;
        private bool isQuitting = false;
        private bool sceneRealoading = false;
        void OnEnable()
        {
            // Workaround around unity automatically destroying objects when changing scenes
            // and complaining about spawning objects in onDestroy of other objects.
            GameEvents.current.onSceneChanging += OnSceneChange;
        }
        void OnDisable()
        {
            GameEvents.current.onSceneChanging -= OnSceneChange;
        }
        private void OnSceneChange()
        {
            sceneRealoading = true;
        }
        void OnApplicationQuit()
        {
            isQuitting = true;
        }

        private void OnDestroy()
        {
            // check is the app is not quitting - exiting to the editor from playing, or the scene is not changing - both of which do
            // not play well with unity. If not, spawn those juicy death aprticles.
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