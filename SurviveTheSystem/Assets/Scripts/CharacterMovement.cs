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

        if(movX > 0 || movX < 0 || movZ > 0 || movZ < 0) {
            controller.velocity = moveVector.normalized * speed * Time.deltaTime;
        } else {
            controller.velocity = new Vector3(0, controller.velocity.y, 0);
        }
    }
}
