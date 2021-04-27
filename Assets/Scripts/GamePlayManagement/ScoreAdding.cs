using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platform
{
    // Used for adding score to player.
    public class ScoreAdding : MonoBehaviour
    {
        public static void AddScoreToPlayer(int Amount)
        {
            GameEvents.current.AddPlayerScore(Amount);
        }
    }
}
