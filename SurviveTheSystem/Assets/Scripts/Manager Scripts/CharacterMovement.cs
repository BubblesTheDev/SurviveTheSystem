using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float acceleration = 1;
    public float deceleration = 2;
    public float maxSpeed = 5;
    CharacterController controller;
    Vector3 moveVector;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0) moveVector.x += Input.GetAxis("Horizontal") * acceleration * Time.deltaTime;
        if (Input.GetAxis("Vertical") != 0) moveVector.z += Input.GetAxis("Vertical") * acceleration * Time.deltaTime;
        if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0) moveVector /= deceleration;
        if (moveVector.magnitude < 0.1f && moveVector.magnitude > -0.1f && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) moveVector = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Vector3.ClampMagnitude(moveVector, maxSpeed);
        Vector3.ClampMagnitude(moveVector, -maxSpeed);
        controller.Move(moveVector);
    }
}
