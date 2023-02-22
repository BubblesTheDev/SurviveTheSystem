using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class breathingManager : MonoBehaviour
{
    [SerializeField] private GameObject targetBox, featherIndicator;
    [SerializeField] private Image sanityBar;
    [SerializeField] private float rangeToBeOk, maxHeight;
    [SerializeField] private float moveSpeed, sanityIncrease, sanityDecrease, currentSanity, maxSanity;
    private playerHealth health;
    [SerializeField] private bool isInBox;
    private void Awake()
    {
        health = GameObject.Find("GameManager").GetComponent<playerHealth>();
    }


    private void Update()
    {
        moveBox();



        //if (Vector3.Distance(targetBox.transform.position, featherIndicator.transform.position) <= rangeToBeOk) isInBox = true;
        //else isInBox = false;
    }

    void moveBox()
    {
        float Y = Mathf.SmoothStep(-maxHeight, maxHeight, Mathf.PingPong(Time.time * moveSpeed, 1));
        targetBox.transform.position = new Vector3(targetBox.transform.position.x, Y+500, targetBox.transform.position.z);
    }

    void moveFeather()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            featherIndicator.transform.position = new Vector3(featherIndicator.transform.position.x, featherIndicator.transform.position.y + moveSpeed * Time.deltaTime, featherIndicator.transform.position.z);

        }
        else
        {
            featherIndicator.transform.position = new Vector3(featherIndicator.transform.position.x, featherIndicator.transform.position.y - moveSpeed * Time.deltaTime, featherIndicator.transform.position.z);
        }
    }
}
