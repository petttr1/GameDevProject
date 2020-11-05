using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PowerUpPickup : MonoBehaviour
    {
        public int type;
        private float moveSpeed = 2f;
        private bool IsUp = false;
        private Vector3 startPosition;
        private Vector3 endPosition;
        private float step;
        private Vector3 moveDir;
        // Start is called before the first frame update
        void Start()
        {
            startPosition = transform.position;
            endPosition = transform.position + new Vector3(0f, 2f, 0f);
            step = moveSpeed * Time.deltaTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsUp)
            {
                moveDown();
                if (transform.position.y <= startPosition.y) IsUp = false;
            }
            else
            {
                moveUp();
                if (transform.position.y >= endPosition.y) IsUp = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject player = other.gameObject;
            player.GetComponent<PowerUps>().AddPowerUp(type);
            Destroy(gameObject);
        }

        private void moveUp()
        {
            transform.Translate(transform.InverseTransformVector(new Vector3(0, step, 0)));
            // transform.position = Vector3.Lerp(startPosition, endPosition, step);
        }

        private void moveDown()
        {
            transform.Translate(transform.InverseTransformVector(new Vector3(0, -step, 0)));
            // transform.position = Vector3.Lerp(endPosition, startPosition, step);
        }
    }
}
