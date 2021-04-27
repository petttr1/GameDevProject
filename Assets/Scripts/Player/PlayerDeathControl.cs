using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platform
{
    // Manages the death screen. Shows the UI element ans final score.
    [RequireComponent(typeof(GamePauseControl))]
    public class PlayerDeathControl : MonoBehaviour
    {
        public GameObject MenuUI;

        private void Start()
        {
            GameEvents.current.onPlayerDeath += PlayerDeath;
            MenuUI.SetActive(false);
        }

        private void OnDestroy()
        {
            GameEvents.current.onPlayerDeath -= PlayerDeath;
        }

        public void PlayerDeath()
        {
            GetComponent<GamePauseControl>().DoPause(false);
            MenuUI.SetActive(true);
        }
    }
}
