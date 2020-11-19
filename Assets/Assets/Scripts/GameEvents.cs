using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class GameEvents
    {
        private static GameEvents instance = null;
        public static GameEvents current
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEvents();
                }
                return instance;
            }
        }

        public event Action<GameObject> onPlayerPlatformLand;
        public event Action onPlayerDeath;

        public void PlayerPlatformLand(GameObject platform)
        {
            onPlayerPlatformLand?.Invoke(platform);
        }

        public void PlayerDeath()
        {
            onPlayerDeath?.Invoke();
        }
    }
}
