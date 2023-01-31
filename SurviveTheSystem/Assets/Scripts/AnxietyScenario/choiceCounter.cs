using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choiceCounter : MonoBehaviour
{
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


}
