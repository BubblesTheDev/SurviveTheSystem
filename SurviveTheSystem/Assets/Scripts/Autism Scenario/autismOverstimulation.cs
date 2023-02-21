using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autismOverstimulation : MonoBehaviour
{
    [Header("Announcments")]
    public AudioSource musicSource;
    public AudioSource announcmentSource;
    public List<AudioClip> announcements;
    public float minTimeBetweenAnnouncments, maxTimeBetweenAnnouncements;
    float currentAnnouncementTime;

    private void Awake()
    {
        StartCoroutine(waitForAnnouncement());
    }

    private void Update()
    {
        
    }

    public IEnumerator waitForAnnouncement()
    {
        yield return new WaitForSeconds(Random.Range(10, 20));
        StartCoroutine(playAnnouncement());
    }

    public IEnumerator playAnnouncement()
    {

        currentAnnouncementTime = Random.Range(minTimeBetweenAnnouncments, maxTimeBetweenAnnouncements);

        announcmentSource.clip = announcements[Random.Range(0, announcements.Count)];
        announcmentSource.Play();
        musicSource.Pause();
        yield return new WaitForSeconds(announcmentSource.clip.length);

        musicSource.UnPause();
        yield return new WaitForSeconds(currentAnnouncementTime);




        StartCoroutine(playAnnouncement());
    }
}
