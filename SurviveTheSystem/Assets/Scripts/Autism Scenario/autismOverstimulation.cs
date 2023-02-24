using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autismOverstimulation : MonoBehaviour
{
    [Header("Announcments")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource announcmentSource;
    [SerializeField] private List<AudioClip> announcements;
    [SerializeField] private float minTimeBetweenAnnouncments, maxTimeBetweenAnnouncements;
    float currentAnnouncementTime;

    [Header("Random Popup ads")]
    [SerializeField] private List<GameObject> adPlacements;
    [SerializeField] private List<GameObject> adsToSpawn;
    [SerializeField] private float minTimeBetweenAds, maxTimeBetweenAds;
    float currentTimeBetweenAds;
    [SerializeField] private bool adIsSpawned;
    [SerializeField] private CharacterMovement playerMovement;

    private void Awake()
    {
        StartCoroutine(waitForAnnouncement());
        StartCoroutine(spawnAd());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F1) && adIsSpawned) GameObject.Find("AdHolder").GetComponentInChildren<adObject>().closeAd();
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

    public IEnumerator spawnAd()
    {
        currentTimeBetweenAds = Random.Range(minTimeBetweenAds, maxTimeBetweenAds);
        yield return new WaitForSeconds(currentTimeBetweenAds);

        playerMovement.canMove = false;
        GameObject temp = Instantiate(adsToSpawn[Random.Range(0, adsToSpawn.Count)], adPlacements[Random.Range(0, adPlacements.Count)].transform.position, Quaternion.identity, GameObject.Find("AdHolder").transform);
        temp.transform.LookAt(GameObject.Find("Player").transform);
        GetComponent<breathingManager>().currentSanity /= Random.Range(1.5f, 2f);
    }
}
