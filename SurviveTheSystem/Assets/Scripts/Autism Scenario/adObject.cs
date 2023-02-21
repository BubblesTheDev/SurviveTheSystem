using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adObject : MonoBehaviour
{
    public AudioSource closeSource;

    public void closeAd()
    {
        Destroy(transform.Find("CloseButton").gameObject);
        closeSource.Play();
        Destroy(gameObject, closeSource.clip.length);
        StartCoroutine(GameObject.Find("GameManager").GetComponent<autismOverstimulation>().spawnAd());
        StartCoroutine(GameObject.Find("GameManager").GetComponent<autismOverstimulation>().spawnAd());
    }
}
