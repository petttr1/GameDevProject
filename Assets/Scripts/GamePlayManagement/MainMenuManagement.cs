using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
    public class MainMenuManagement : MonoBehaviour
    {
        public GameObject TutorialScreen;
        public GameObject MainMenuScreen;

        public void Awake()
        {
            TutorialScreen.SetActive(false);
        }

        public void Play()
        {
            SceneManager.LoadScene(1);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void Tutorial()
        {
            MainMenuScreen.SetActive(false);
            TutorialScreen.SetActive(true);
        }

        public void TutorialBack()
        {
            TutorialScreen.SetActive(false);
            MainMenuScreen.SetActive(true);
        }
            
    }
}