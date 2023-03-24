using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPull : MonoBehaviour
{
    public static cameraPull cameraPullScript;

    public float currentPullForce, pullForceIncrease;
    public float offsetToStop = 5f;
    public float timeToPull;
    float currentTime;
    public bool isPulling;

    private void Awake() {
        cameraPullScript = this;
    }





    public IEnumerator pullToObject(GameObject objToPullTo, GameObject objToPull) {
        while (objToPull.transform.rotation != objToPullTo.transform.rotation) {
            Quaternion.Lerp(Quaternion.LookRotation(objToPullTo.transform.position - objToPull.transform.position, Vector3.up), objToPull.transform.rotation, currentPullForce);
            currentPullForce *= pullForceIncrease * Time.deltaTime;
            yield return new WaitForEndOfFrame();

            currentTime += Time.deltaTime;
            if(currentTime >= timeToPull) {
                currentTime = 0;
                yield return null;
            }
        }
    }
}
