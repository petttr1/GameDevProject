using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    // Adds hovering effect to a component.
    public class PickupHover : MonoBehaviour
    {
        private float moveSpeed = 2f;
        private bool IsUp = false;
        private Vector3 startPosition;
        private Vector3 endPosition;

        void Start()
        {
            startPosition = transform.position;
            endPosition = transform.position + new Vector3(0f, 2f, 0f);
        }
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

        private void moveUp()
        {
            transform.Translate(transform.InverseTransformVector(new Vector3(0, moveSpeed * Time.deltaTime, 0)));
        }

        private void moveDown()
        {
            transform.Translate(transform.InverseTransformVector(new Vector3(0, -moveSpeed * Time.deltaTime, 0)));
        }
    }
}