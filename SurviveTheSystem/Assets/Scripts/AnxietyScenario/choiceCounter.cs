using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choiceCounter : MonoBehaviour
{
    public speechHandeler handeler;
    public bool isGood;
    GameObject player;

    private void Awake()
    {
        handeler = GameObject.Find("GameManager").GetComponent<speechHandeler>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        transform.LookAt(player.transform);
    }


}
