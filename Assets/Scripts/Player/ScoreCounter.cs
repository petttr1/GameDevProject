using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platform
{
    public class ScoreCounter : MonoBehaviour
    {
        private int Score = -1050;
        private TextMesh ScoreDisplay;
        void Start()
        {

            ScoreDisplay = GetComponentInChildren<TextMesh>();
            DisplayScore();
            GameEvents.current.onAddScore += AddScore;
            GameEvents.current.onPlayerDeath += PlayerDeath;
        }

        private void OnDestroy()
        {
            GameEvents.current.onAddScore -= AddScore;
            GameEvents.current.onPlayerDeath -= PlayerDeath;
        }

        private void AddScore(int amount)
        {
            Score += amount;
            DisplayScore();
        }

        private void DisplayScore()
        {
            ScoreDisplay.text = Score.ToString();
        }

        void PlayerDeath ()
        {
            GameEvents.current.SetFinalScore(Score);
        }
    }
}