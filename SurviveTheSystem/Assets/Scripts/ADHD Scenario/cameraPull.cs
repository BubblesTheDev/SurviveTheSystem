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

    private void Update()
    {
        
    }

    public void pullCamera()
    {

    }

    public IEnumerator pullToObject(GameObject objToPullTo, GameObject objToPull) {
        while (true) {
            
            Quaternion.Lerp(objToPullTo.transform.rotation, objToPull.transform.rotation, currentPullForce);
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
