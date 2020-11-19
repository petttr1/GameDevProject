using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class GamePauseControl : MonoBehaviour
    {
        [SerializeField] private GameObject MenuUI;
        public static bool GamePaused = false;

        private void Start()
        {
            MenuUI.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                GamePaused = GamePaused ? Resume() : Pause();
            }
        }

        private bool Pause()
        {
            MenuUI.SetActive(true);
            AudioListener.pause = true;
            Time.timeScale = 0f;
            return !GamePaused;
        }

        private bool Resume()
        {
            MenuUI.SetActive(false);
            AudioListener.pause = false;
            Time.timeScale = 1f;
            return !GamePaused;
        }

        public void DoResume()
        {
            GamePaused = Resume();
        }
    }

}