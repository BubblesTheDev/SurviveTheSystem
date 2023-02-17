using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choiceCounter : MonoBehaviour
{
    public float minRotSpeed, maxRotSpeed;
    public float rotAngleY;
    float rotSpeed;


    public speechHandeler handeler;
    public bool isGood;
    GameObject player;
    public AudioClip storedClip;

    private void Awake()
    {
        handeler = GameObject.Find("GameManager").GetComponent<speechHandeler>();
        player = GameObject.Find("Player");

        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position) ;
    }


    private void Update() {
        float rY = Mathf.SmoothStep(-rotAngleY, rotAngleY, Mathf.PingPong(Time.time * rotSpeed, 1));
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, rY);
    }

}
