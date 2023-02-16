using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5;
    Rigidbody controller;
    public Vector3 moveVector;

    float movX, movZ;

    private void Awake()
    {
        controller = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        moveVector = transform.forward * movZ + transform.right * movX;

        controller.velocity = moveVector.normalized * speed;
    }
}
