using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainExperience : experienceBase
{
    public ParticleSystem rainParticle;
    public bool isActive;

    public override void playExperience(string obj)
    {
        isActive = true;
        rainParticle.Play();
        soundManager.playClip(obj);
        StartCoroutine(cameraPull.cameraPullScript.pullToObject(Camera.main.gameObject, GameObject.Find("Rain Particles")));
    }
}
