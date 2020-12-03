using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
    [RequireComponent(typeof(GamePauseControl))]
    public class MenuInteractionsManagement : MonoBehaviour
    {
        public void Resume()
        {
            GetComponent<GamePauseControl>().DoResume();
        }
        public void Restart()
        {
            GetComponent<GamePauseControl>().DoResume();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        public void MainMenu()
        {
            GetComponent<GamePauseControl>().DoResume();
            SceneManager.LoadScene(0);
        }
    }
}
