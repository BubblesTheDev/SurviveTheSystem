using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBackAndForthVertical : MonoBehaviour
{
    public float minRotSpeed, maxRotSpeed;
    public float rotAngleY;
    float rotSpeed;
    public Vector3 starterRot;

    private void Awake()
    {
        rotSpeed = Random.Range(minRotSpeed, maxRotSpeed);
        starterRot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float rY = Mathf.SmoothStep(-rotAngleY, rotAngleY, Mathf.PingPong(Time.time * rotSpeed, 1));
        transform.rotation = Quaternion.Euler(0, rY+starterRot.y, 0);
    }
}
