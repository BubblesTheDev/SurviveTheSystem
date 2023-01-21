using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5;
    CharacterController controller;
    public Vector3 moveVector;

    float movX, movZ;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    private void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        moveVector = transform.forward * movZ + transform.right * movX;
        

        controller.Move(moveVector.normalized * Time.deltaTime * speed);
    }

    private void FixedUpdate()
    {
        
    }
}
