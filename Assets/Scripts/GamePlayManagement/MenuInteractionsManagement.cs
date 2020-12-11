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
            GameEvents.current.SceneChange();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            GetComponent<GamePauseControl>().DoResume();
        }
        public void MainMenu()
        {
            GameEvents.current.SceneChange();
            SceneManager.LoadScene(0);
            GetComponent<GamePauseControl>().DoResume();
        }
    }
}
