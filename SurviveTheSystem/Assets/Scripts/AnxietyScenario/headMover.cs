using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headMover : MonoBehaviour
{
    public GameObject objToAim;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 aim = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 15));

        objToAim.transform.LookAt(aim);
    }
}
