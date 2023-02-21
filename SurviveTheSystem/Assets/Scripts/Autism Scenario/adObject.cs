using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adObject : MonoBehaviour
{
    public AudioSource closeSource;

    public void closeAd()
    {
        GameObject.Find("Player").GetComponent<CharacterMovement>().canMove = true;
        StartCoroutine(GameObject.Find("GameManager").GetComponent<autismOverstimulation>().spawnAd());
        closeSource.Play();
    }
}
