using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choiceCounter : MonoBehaviour
{
    public float minRotSpeed, maxRotSpeed;
    public float rotAngleY;
    float rotSpeed;
    public bool playerIsLooking;


    public speechHandeler handeler;
    public bool isGood;
    GameObject player;
    public AudioClip storedClip;

    private void Awake()
    {
        handeler = GameObject.Find("GameManager").GetComponent<speechHandeler>();
        player = GameObject.Find("Player");
        rotSpeed = Random.Range(minRotSpeed, maxRotSpeed);
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position) ;
    }


    private void Update() {
        float rY = Mathf.SmoothStep(-rotAngleY, rotAngleY, Mathf.PingPong(Time.time * rotSpeed, 1));
        if (!playerIsLooking) transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, rY);
        else transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

    }

    private void LateUpdate()
    {
        playerIsLooking = false;
    }
}
