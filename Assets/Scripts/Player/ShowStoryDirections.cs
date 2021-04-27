using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // After called ("E"), spawns the navuigator element, which shows the directions.
    public class ShowStoryDirections : MonoBehaviour
    {
        public float sacrificeRate = 20f;
        public GameObject Navigator;

        private Vector3 navigatingTo;

        void Start()
        {
            GameEvents.current.onNewStoryPlatform += setNewDestination;
            navigatingTo = new Vector3(0f, 0f, 0f);
        }

        private void OnDestroy()
        {
            GameEvents.current.onNewStoryPlatform -= setNewDestination;
        }

        void Update()
        {
            if (!GamePauseControl.GamePaused && Input.GetButtonDown("Submit"))
            {
                Navigate();
            }
        }
        
        private void Navigate()
        {
            // subtract the sacrificed light
            gameObject.GetComponentInChildren<Lightness>().DealDamage(sacrificeRate);
            // spawn the navigator
            GameObject nav = Instantiate(Navigator, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            // point it in the right direction
            nav.GetComponent<Navigate>().setDirection(navigatingTo);
        }
        // When landing on a new sotry platfrom, set new dest for the navigator.
        private void setNewDestination(Transform newStoryPlatform)
        {
            navigatingTo = newStoryPlatform.position;
        }
    }
}
