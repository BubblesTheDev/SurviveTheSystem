using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class experienceHandler : MonoBehaviour {
    public static experienceHandler current;
    public List<experienceStats> experiences;
    [SerializeField] private float minTimeBetweenExp, maxTimeBetweenExp;
    [SerializeField] private bool isPlaying;
    private float currentTime;

    private void Awake() {
        current = this;
    }

    public IEnumerator callExperience(string experienceToCall) {

        isPlaying = true;

        //finds the current experience where its name matches the experience to call, then calls its spesific action
        current.experiences.Where(experiences => experiences.name == experienceToCall).FirstOrDefault().experienceObject.playExperience(experienceToCall);
        if (current.experiences.Where(experiences => experiences.name == experienceToCall).FirstOrDefault().isLimited) experiences.Remove(current.experiences.Where(experiences => experiences.name == experienceToCall).FirstOrDefault());


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
