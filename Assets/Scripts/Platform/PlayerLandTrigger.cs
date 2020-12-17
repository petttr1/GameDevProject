using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Player landing on platform is used on multiple occasions (obviously)
    public class PlayerLandTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameEvents.current.PlayerPlatformLand(gameObject, other.transform.forward);
        }
    }
}