using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPull : MonoBehaviour
{
    public GameObject objToPullTo;
    public GameObject objToPull;
    public float currentPullForce, pullForceIncrease;
    public bool isPulling;



    private void Update() {
        if(objToPull != null && isPulling) {
            pullToObject();
        }
    }



    void pullToObject() {
        Quaternion.Lerp(Quaternion.LookRotation(objToPullTo.transform.position - objToPull.transform.position, Vector3.up), objToPull.transform.rotation, currentPullForce);
        currentPullForce *= pullForceIncrease;
    }
}
