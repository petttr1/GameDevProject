using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platform
{
    // Displays the final score on Death Screen after the player dies.
    public class DisplayFinalScore : MonoBehaviour
    {
        private Text textComponent;
        private void Start()
        {
            GameEvents.current.setFinalScore += PlayerDeath;
            textComponent = GameObject.Find("Score").GetComponent<Text>();
        }
        private void OnDestroy()
        {
            GameEvents.current.setFinalScore -= PlayerDeath;
        }
        void PlayerDeath(int score)
        {
            textComponent.text = score.ToString();
        }
    }
}