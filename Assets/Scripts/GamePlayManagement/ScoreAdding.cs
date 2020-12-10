using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platform
{
    public class ScoreAdding : MonoBehaviour
    {
        // Start is called before the first frame update
        public static void AddScoreToPlayer(int Amount)
        {
            GameEvents.current.AddPlayerScore(Amount);
        }
    }
}
