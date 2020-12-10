using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform {
    public class PlayRandomPlaylist : MonoBehaviour
    {
        public AudioClip[] TracksToPlay;
        public AudioSource audioSource;

        void Start()
        {
            audioSource.clip = TracksToPlay[Random.Range(0, TracksToPlay.Length)];
            audioSource.Play();
        }

        void Update()
        {
            if (!audioSource.isPlaying && !GamePauseControl.GamePaused)
            {
                audioSource.clip = TracksToPlay[Random.Range(0, TracksToPlay.Length)];
                audioSource.Play();
            }
        }
    }

}
