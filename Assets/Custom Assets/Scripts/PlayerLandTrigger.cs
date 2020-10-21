using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameEvents.current.PlayerPlatformLand(gameObject);
    }
}
