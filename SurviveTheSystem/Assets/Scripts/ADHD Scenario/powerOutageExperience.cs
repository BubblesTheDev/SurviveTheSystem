using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerOutageExperience : experienceBase
{

    public GameObject[] lights;
    public GameObject rainLight;
    public List<GameObject> people;
    public override void playExperience(string obj)
    {
        if(GameObject.Find("Lesson") != null) soundManager.togglePause("lesson");
        soundManager.playClip("whispers");
        soundManager.playClip(obj);

        foreach (GameObject item in lights)
        {
            item.SetActive(false);
        }

        rainLight.SetActive(true);

        StartCoroutine(classConfusion());



        
    }


    public IEnumerator classConfusion()
    {
        foreach (GameObject item in people)
        {
            item.GetComponent<rotateBackAndForthVertical>().enabled = true;
        }

        yield return new WaitForSeconds(10f);

        foreach (GameObject item in people)
        {
            item.GetComponent<rotateBackAndForthVertical>().enabled = false;
            item.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }

        soundManager.togglePause("whispers");
        soundManager.togglePause("lesson");


        yield return new WaitForSeconds(Random.Range(25,40f));

        foreach (GameObject item in lights)
        {
            item.SetActive(true);
        }

        rainLight.SetActive(false);
    }
}
