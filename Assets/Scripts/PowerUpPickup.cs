using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PowerUpPickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameObject player = other.gameObject;
            player.GetComponent<Dash>().AddPowerUp();
            Destroy(gameObject);
        }
    }
}
