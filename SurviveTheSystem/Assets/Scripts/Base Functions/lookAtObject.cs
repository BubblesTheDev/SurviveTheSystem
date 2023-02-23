using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtObject : MonoBehaviour
{
    public GameObject objToLookAt;
    public bool lookAway;

    private void Update() {
        if (lookAway) transform.rotation = Quaternion.LookRotation(transform.position - objToLookAt.transform.position);
        else transform.LookAt(objToLookAt.transform.position);

    }
}
