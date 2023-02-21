using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToObject : MonoBehaviour
{
    public GameObject targetObj;

    void Update()
    {
        transform.position = targetObj.transform.position;
        
    }
}
