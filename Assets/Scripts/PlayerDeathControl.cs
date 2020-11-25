﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlayerDeathControl : MonoBehaviour
    {
        [SerializeField] private GameObject MenuUI;

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
            gameObject.GetComponent<GamePauseControl>().DoPause(false);
            MenuUI.SetActive(true);
        }
    }
}