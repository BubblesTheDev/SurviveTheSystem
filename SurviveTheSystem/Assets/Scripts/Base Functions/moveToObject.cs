using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToObject : MonoBehaviour
{
    public GameObject targetObj;
    // Update is called once per frame
    void Update()
    {
        transform.position = targetObj.transform.position;
    }
}
