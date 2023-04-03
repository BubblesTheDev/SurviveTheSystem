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

    private void Awake() {
        current = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1)) StartCoroutine(callExperience("Rain"));
        if (Input.GetKeyUp(KeyCode.F2)) StartCoroutine(callExperience("Phone"));
        if (Input.GetKeyUp(KeyCode.F3)) StartCoroutine(callExperience("Power"));
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
}

[System.Serializable]
public struct experienceStats {
    public string name;
    public experienceBase experienceObject;
    public float experienceChance;
    public bool isLimited;
}
