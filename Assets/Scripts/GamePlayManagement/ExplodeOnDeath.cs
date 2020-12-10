using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplodeOnDeath : MonoBehaviour
{
    public GameObject onDestroyParticles;
    private bool isQuitting = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isQuitting = true;
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            ParticleSystem ps = onDestroyParticles.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            MeshRenderer rend = gameObject.GetComponent<MeshRenderer>();
            //if (rend == null)
            //{
            //    rend = gameObject.GetComponentInChildren<MeshRenderer>();
            //}
            psmain.startColor = rend.material.color;
            Instantiate(onDestroyParticles, gameObject.transform.position, Quaternion.identity);
        }
    }

}
