using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class experienceHandler : MonoBehaviour {
    public static experienceHandler current;
    public experienceStats[] experiences;
    [SerializeField] private float minTimeBetweenExp, maxTimeBetweenExp;
    [SerializeField] private bool isPlaying;
    private float currentTime;
    public float scenarioTime;

    private void Awake() {
        current = this;
        StartCoroutine(sceneTimer());
        isPlaying = true;

        StartCoroutine(startingTime());
    }

    private void Update()
    {


        if (!isPlaying) StartCoroutine(callExperience(experiences[UnityEngine.Random.Range(0, experiences.Length)].name));
    }

    public IEnumerator callExperience(string experienceToCall) {

        isPlaying = true;

        //finds the current experience where its name matches the experience to call, then calls its spesific action
        Array.Find(current.experiences, experienceStats => experienceStats.name == experienceToCall).experienceObject.playExperience(experienceToCall);
        yield return new WaitForSeconds(10f);

        currentTime = UnityEngine.Random.Range(minTimeBetweenExp, maxTimeBetweenExp);
        yield return new WaitForSeconds(currentTime);
        isPlaying = false;
    }

    public IEnumerator startingTime()
    {
        yield return new WaitForSeconds(10f);
        isPlaying = false;
    }

    public IEnumerator sceneTimer()
    {
        yield return new WaitForSeconds(240f);
        soundManager.playClip("bell");
        yield return new WaitForSeconds(4f);
        sceneManagmentFunctions.loadScene(0);
    }
}

[System.Serializable]
public struct experienceStats {
    public string name;
    public experienceBase experienceObject;
    public bool isLimited;
}
