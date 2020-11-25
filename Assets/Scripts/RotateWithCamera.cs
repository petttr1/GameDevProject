using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    public float rotateSpeed = 5;
    public float ShooterRadius = 0.5f;
    private Transform cam;
    private Vector3 rotationCenter;
    private float cam_angle;
    private Vector3 cam_pos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        rotationCenter = GetComponentInParent<Transform>().position;

        rotationCenter.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        cam_pos = transform.InverseTransformPoint(cam.position);
        cam_pos.y = 0;
        cam_angle = Mathf.Atan(cam_pos.x / cam_pos.z);
        transform.RotateAround(rotationCenter, Vector3.up, cam_angle);
    }
}
