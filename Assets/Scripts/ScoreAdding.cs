using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class ScoreAdding : MonoBehaviour
    {
        public int Amount = 10;
        // Start is called before the first frame update
        public void AddScoreToPlayer()
        {
            GameEvents.current.AddPLayerScore(Amount);
        }
    }
}
