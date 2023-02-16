using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyOverstimulation : MonoBehaviour
{
    public List<ParticleSystem> cameraFlashes;
    public List<AudioClip> cameraSounds;
    public float minTime, maxTime;
    public cameraFlasher[] flashers = new cameraFlasher[3];
    


    private playerHealth health;

    private void Awake()
    {
        health = GetComponent<playerHealth>();

        
    }

    private void Update()
    {
        switch (health.currentHearts)
        {
            case 1:
                if(flashers.Length != 7)
                {
                    flashers = new cameraFlasher[7];
                    initalizeFlashers();
                }
                break;
            case 2:
                if (flashers.Length != 4)
                {
                    flashers = new cameraFlasher[4];
                    initalizeFlashers();
                }
                break;
            case 3:
                if (flashers.Length != 2)
                {
                    flashers = new cameraFlasher[2];
                    initalizeFlashers();
                }
                break;
        }
    }

    public void initalizeFlashers()
    {
        for (int i = 0; i < flashers.Length - 1; i++)
        {
            flashers[i] = new cameraFlasher(minTime, maxTime);
            StartCoroutine(flashCamera(flashers[i]));
        }
    }

    public IEnumerator flashCamera(cameraFlasher flasher)
    {
        int temp = Random.Range(0, cameraFlashes.Count);
        cameraFlashes[temp].Play();
        cameraFlashes[temp].gameObject.GetComponent<AudioSource>().clip = cameraSounds[Random.Range(0,cameraSounds.Count)];
        cameraFlashes[temp].gameObject.GetComponent<AudioSource>().Play();

        flasher.timeToPickupItem = Random.Range(flasher.timeBetweenMin, flasher.timeBetweenMax);

        yield return new WaitForSeconds(flasher.timeToPickupItem);

        StartCoroutine(flashCamera(flasher));
    }

}

public struct cameraFlasher
{
    public float timeBetweenMax, timeBetweenMin;
    public float timeToPickupItem;


    public cameraFlasher(float minTime, float maxTime)
    {
        timeBetweenMin = minTime;
        timeBetweenMax = maxTime;
        timeToPickupItem = 0;
    }
}

