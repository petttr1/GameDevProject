using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class GamePauseControl : MonoBehaviour
    {
        public GameObject MenuUI;
        public static bool GamePaused = false;

        private void Start()
        {
            MenuUI.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                GamePaused = GamePaused ? Resume() : Pause(true);
            }
        }

        private bool Pause(bool showMenu)
        {
            if (showMenu) MenuUI.SetActive(true);
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

        public void DoPause(bool showMenu)
        {
            GamePaused = Pause(showMenu);
        }
    }

}