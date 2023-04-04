using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class soundManager : MonoBehaviour
{

    public static soundManager current;


    public GameObject soundsHolder;
    public sound[] soundsInScene;



    private void Awake()
    {
        current = this;
    }
    public static void playClip(string soundToPlay) {
        GameObject soundsHolder = GameObject.Find("soundsHolder");

        if (soundsHolder.transform.Find(soundToPlay) == null) {
            GameObject temp = new GameObject(soundToPlay);
            temp.transform.parent = soundsHolder.transform;
            temp.AddComponent<AudioSource>();
            temp.GetComponent<AudioSource>().playOnAwake = false;
        }

        soundsHolder.transform.Find(soundToPlay).GetComponent<AudioSource>().loop = Array.Find(soundManager.current.soundsInScene, sound => sound.clipName == soundToPlay).loopClip;
        soundsHolder.transform.Find(soundToPlay).GetComponent<AudioSource>().clip = Array.Find(soundManager.current.soundsInScene, sound => sound.clipName == soundToPlay).clipToPlay;
        soundsHolder.transform.Find(soundToPlay).GetComponent<AudioSource>().Play();
    }


    public static void togglePause(string soundToPause)
    {
        GameObject soundsHolder = GameObject.Find("soundsHolder");
        if (soundsHolder.transform.Find(soundToPause))
        {
            if (soundsHolder.transform.Find(soundToPause).GetComponent<AudioSource>().isPlaying) soundsHolder.transform.Find(soundToPause).GetComponent<AudioSource>().Pause();
            else soundsHolder.transform.Find(soundToPause).GetComponent<AudioSource>().UnPause();
        }
    }
}

[System.Serializable]
public struct sound {
    public string clipName;
    public AudioClip clipToPlay;
    public bool loopClip;

}