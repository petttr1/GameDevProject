using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Manages pausing the game. Pressing the "cancel" (esc most likely) key pauses the game, shows the menu and all the other good stuff.
    public class GamePauseControl : MonoBehaviour
    {
        public GameObject MenuUI;
        public static bool GamePaused = false;

        private void Start()
        {
            MenuUI.SetActive(false);
            DoResume();
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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            return true;
        }

        private bool Resume()
        {
            MenuUI.SetActive(false);
            AudioListener.pause = false;
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            return false;
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