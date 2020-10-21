using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake() {
        current = this;
    }

    public event Action<GameObject> onPlayerPlatformLand;

    public void PlayerPlatformLand(GameObject platform) {
        if (onPlayerPlatformLand != null) {
            onPlayerPlatformLand(platform);
        }
    }
}
