using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class experienceHandler : MonoBehaviour
{
    public static experienceHandler current;
    public List<experienceStats> experiences;

    private void Awake()
    {
        current = this;
    }

    public event Action<string> onExpereienceCall;
    public void callExperience(string experienceToCall)
    {
        //finds the current experience where its name matches the experience to call, then calls its spesific action
        //current.experiences.Where(experiences => experiences.name == experienceToCall).FirstOrDefault().experienceObject.playExperience();
    }
}

[System.Serializable]
public struct experienceStats
{
    public string name;
    public experienceBase experienceObject;
    public bool isPlaying;
    public float experienceChance;
}
