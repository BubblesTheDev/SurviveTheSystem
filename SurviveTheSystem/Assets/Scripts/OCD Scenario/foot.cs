using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foot : MonoBehaviour
{
    public stepHandeler handeler;
    private void Awake()
    {
        handeler = GameObject.Find("Game Manager").GetComponent<stepHandeler>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Crack") && gameObject == handeler.objectToMove) handeler.increaseStep(true);
        else if(gameObject == handeler.objectToMove) handeler.increaseStep(false);
    }
}
