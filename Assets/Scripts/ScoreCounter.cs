using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class ScoreCounter : MonoBehaviour
    {
        private int Score;
        // Start is called before the first frame update
        void Start()
        {
            GameEvents.current.onAddScore += AddScore;
        }

        private void OnDestroy()
        {
            GameEvents.current.onAddScore -= AddScore;
        }

        private void AddScore(int amount)
        {
            Score += amount;
        }
    }
}