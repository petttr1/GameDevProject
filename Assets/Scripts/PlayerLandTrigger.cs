﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlayerLandTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameEvents.current.PlayerPlatformLand(gameObject, other.transform.forward);
        }
    }
}