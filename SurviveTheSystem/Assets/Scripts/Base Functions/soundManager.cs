using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public GameObject soundsHolder;
    public static void playClip(sound soundToPlay) {
        if (!GameObject.Find("soundsHolder").transform.Find(soundToPlay.clipName)) {
            GameObject temp = new GameObject(soundToPlay.clipName);
            temp.transform.parent = GameObject.Find("soundsHolder").transform;
            temp.AddComponent<AudioSource>();
            temp.GetComponent<AudioSource>().playOnAwake = false;
        }

        GameObject.Find("soundsHolder").transform.Find(soundToPlay.clipName).GetComponent<AudioSource>().loop = soundToPlay.loopClip;
        GameObject.Find("soundsHolder").transform.Find(soundToPlay.clipName).GetComponent<AudioSource>().clip = soundToPlay.clipToPlay;
        GameObject.Find("soundsHolder").transform.Find(soundToPlay.clipName).GetComponent<AudioSource>().Play();
    }
}

public struct sound {
    public string clipName;
    public AudioClip clipToPlay;
    public bool loopClip;

}