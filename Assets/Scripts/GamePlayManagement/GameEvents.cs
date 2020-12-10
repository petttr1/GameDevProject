﻿using System;
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

        public event Action<GameObject, Vector3> onPlayerPlatformLand;
        public event Action<GameObject> onDealDamagePlatforms;
        public event Action<Vector3> onPlayerJump;
        public event Action onPlayerDeath;
        public event Action<Transform> onNewStoryPlatform;
        public event Action<int> onAddScore;

        public void PlayerPlatformLand(GameObject platform, Vector3 playerFacingDirection)
        {
            onPlayerPlatformLand?.Invoke(platform, playerFacingDirection);
        }
        public void DealDamagePlatforms(GameObject activePlatform)
        {
            onDealDamagePlatforms?.Invoke(activePlatform);
        }
        public void PlayerJump(Vector3 originalVelocity)
        {
            onPlayerJump?.Invoke(originalVelocity);
        }
        public void PlayerDeath()
        {
            onPlayerDeath?.Invoke();
        }
        public void NewStoryPlatform(Transform newPlatform)
        {
            onNewStoryPlatform?.Invoke(newPlatform);
        }
        public void AddPlayerScore(int score)
        {
            onAddScore?.Invoke(score);
        }
    }
}