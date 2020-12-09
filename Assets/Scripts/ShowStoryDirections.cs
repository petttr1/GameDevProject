using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(AudioSource))]
    public class ShowStoryDirections : MonoBehaviour
    {
        public float sacrificeRate = 5f;
        public AudioClip NavSound;

        private Vector3 navigatingTo;
        LineRenderer rend;
        // Start is called before the first frame update
        void Start()
        {
            GameEvents.current.onNewStoryPlatform += setNewDestination;
            navigatingTo = new Vector3(0f, 0f, 0f);
            rend = GetComponentInChildren<LineRenderer>();
            rend.enabled = false;
        }

        private void OnDestroy()
        {
            GameEvents.current.onNewStoryPlatform -= setNewDestination;
        }
        // Update is called once per frame
        void Update()
        {
            if (!GamePauseControl.GamePaused && Input.GetButtonDown("Submit"))
            {
                Navigate();
            }
            if (!GamePauseControl.GamePaused && Input.GetButtonUp("Submit")) 
            {
                StopNavigate();
            }
        }
        
        private void Navigate()
        {
            // play navigating sounds
            AudioSource.PlayClipAtPoint(NavSound, gameObject.transform.position + 10* (gameObject.transform.forward));
            rend.SetPosition(0, transform.position + new Vector3(0f, 0.5f, 0f));
            rend.SetPosition(1, navigatingTo);
            rend.enabled = true;
            gameObject.GetComponentInChildren<Lightness>().DealDamage(Time.deltaTime * sacrificeRate);
        }

        private void StopNavigate()
        {
            rend.enabled = false;
        }

        private void setNewDestination(Transform newStoryPlatform)
        {
            navigatingTo = newStoryPlatform.position;
        }
    }
}
