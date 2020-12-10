using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlatformDespawner : MonoBehaviour
    {
        public void Despawn()
        {
            Destroy(gameObject);
        }
    }
}
