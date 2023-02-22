using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class speechHandeler : MonoBehaviour
{

    [Header("Speech Settings")]
    public GameObject choiceSpawningBasePos;
    public float choiceSize;
    public Bounds spawnBounds;
    public int speechPartIndex;
    public List<speechPart> speechList;
    public GameObject[] brainfog;
    public GameObject choicePrefab;
    private bool isChoosing;
    public TextMeshProUGUI speechText;



    [Space, Header("Choosing Settings")]
    public GameObject HeadPos;
    public float rayCastThickness;
    public LayerMask layersToHit;
    public float timeToChoose;
    float timeToPickupItem, timeToPickupItem2 = 7;
    RaycastHit hit;
    public Image choosingBar;
    public Image timeToChooseBar;
    private playerHealth health;

    public AudioSource audioPlayer;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        health = GetComponent<playerHealth>();


        StartCoroutine(playAudioStart());
    }


    private void Update()
    {
        finishSpeech();

        chooseObject();
        choosingBar.fillAmount = timeToPickupItem / timeToChoose;
        timeToChooseBar.fillAmount = timeToPickupItem2 / 7f;

        if (isChoosing && timeToPickupItem2 > 0 && !audioPlayer.isPlaying)
        {
            timeToPickupItem2 -= Time.deltaTime;
        }
        else if (timeToPickupItem2 <= 0) StartCoroutine(choiceHasBeenMade(false, speechList[speechPartIndex].badChoices[Random.Range(0, speechList[speechPartIndex].badChoices.Count)].choiceAudio));
    }

    public void changeSpeechText()
    {
        speechText.text = speechList[speechPartIndex].startingPart + " ______ " + speechList[speechPartIndex].endingPart;
    }

    IEnumerator playAudioStart()
    {
        changeSpeechText();
        audioPlayer.clip = speechList[speechPartIndex].startingPartAudio;
        audioPlayer.Play();
        isChoosing = false;
        yield return new WaitForSeconds(audioPlayer.clip.length);
        spawnChoices();

    }


    public void spawnChoices()
    {

        int randomGoodChoiceAmmount = Random.Range(2, speechList[speechPartIndex].goodChoices.Count + 1);
        int randomBadChoiceAmmount = Random.Range(3, speechList[speechPartIndex].badChoices.Count + 1);

        for (int i = 0; i < speechList[speechPartIndex].goodChoices.Count;)
        {
            Vector3 spawnPoint = getRandomPointInBounds(spawnBounds);
            if (!Physics.CheckSphere(spawnPoint, choiceSize))
            {
                GameObject temp = Instantiate(choicePrefab, spawnPoint, Quaternion.identity, transform.Find("Text Canvas"));

                temp.GetComponent<TextMeshProUGUI>().text = speechList[speechPartIndex].goodChoices[i].choiceText;
                temp.GetComponent<choiceCounter>().isGood = true;
                temp.GetComponent<choiceCounter>().storedClip = speechList[speechPartIndex].goodChoices[i].choiceAudio;

                temp.name = speechList[speechPartIndex].goodChoices[i].choiceAudio.name;
                i++;
            }
        }

        for (int i = 0; i < speechList[speechPartIndex].badChoices.Count;)
        {
            Vector3 spawnPoint = getRandomPointInBounds(spawnBounds);
            if (!Physics.CheckSphere(spawnPoint, choiceSize))
            {
                GameObject temp = Instantiate(choicePrefab, spawnPoint, Quaternion.identity, transform.Find("Text Canvas"));

                temp.GetComponent<TextMeshProUGUI>().text = speechList[speechPartIndex].badChoices[i].choiceText;
                temp.GetComponent<choiceCounter>().isGood = false;
                temp.GetComponent<choiceCounter>().storedClip = speechList[speechPartIndex].badChoices[i].choiceAudio;
                temp.name = speechList[speechPartIndex].badChoices[i].choiceAudio.name;
                i++;
            }
        }

        isChoosing = true;

    }


    public Vector3 getRandomPointInBounds(Bounds bound)
    {
        float minX = bound.size.x * -0.5f;
        float minY = bound.size.y * -0.5f;
        float minZ = bound.size.z * -0.5f;

        return (Vector3)choiceSpawningBasePos.transform.TransformPoint(
        new Vector3(Random.Range(minX, -minX),
            Random.Range(minY, -minY),
            Random.Range(minZ, -minZ)));
    }

    public IEnumerator choiceHasBeenMade(bool isGoodChoice, AudioClip choiceClip)
    {
        timeToPickupItem2 = 7f;

        if (!isGoodChoice)
        {
            takeDamage();
        }

        foreach (Transform item in transform.Find("Text Canvas"))
        {
            Destroy(item.gameObject);

        }

        audioPlayer.clip = choiceClip;
        audioPlayer.Play();

        yield return new WaitForSeconds(audioPlayer.clip.length);


        audioPlayer.clip = speechList[speechPartIndex].endingPartAudio;
        audioPlayer.Play();
        isChoosing = false;

        yield return new WaitForSeconds(audioPlayer.clip.length+1);


        speechPartIndex++;
        timeToPickupItem2 = 7f;
        StartCoroutine(playAudioStart());
    }

    public void takeDamage()
    {
        StartCoroutine(health.takeDamage());
        int x = Random.Range(3, 5);
        for (int i = 0; i < x; i++)
        {
            Vector2 spawnPoint = new Vector2(Random.Range(125, Screen.width - 350), Random.Range(125, Screen.height - 350));
            GameObject temp = Instantiate(brainfog[Random.Range(0, brainfog.Length)], spawnPoint, Quaternion.identity, GameObject.Find("Brain Fog Holder").transform);
            temp.transform.localScale = temp.transform.localScale * Random.Range(0.25f, 1f);

        }
    }

    public void chooseObject()
    {

        Physics.SphereCast(HeadPos.transform.position, rayCastThickness, HeadPos.transform.forward, out hit, Mathf.Infinity, layersToHit.value);
        if (hit.transform != null)
        {
            isChoosing = false;
            hit.transform.gameObject.GetComponent<choiceCounter>().playerIsLooking = true;
            timeToPickupItem += Time.deltaTime;
            if (timeToPickupItem > timeToChoose)
            {
                StartCoroutine(choiceHasBeenMade(hit.transform.gameObject.GetComponent<choiceCounter>().isGood, hit.transform.GetComponent<choiceCounter>().storedClip));
            }
        }
        else
        {
            timeToPickupItem = 0;
            isChoosing = true;
        }
    }

    public void finishSpeech()
    {
        if(speechPartIndex >= speechList.Count)
        sceneManagmentFunctions.loadScene(5);
    }

}

[System.Serializable]
public struct speechPart
{
    public string startingPart, endingPart;
    public AudioClip startingPartAudio, endingPartAudio;
    public List<speechChoice> goodChoices;
    public List<speechChoice> badChoices;
}
[System.Serializable]
public struct speechChoice
{
    public string choiceText;
    public AudioClip choiceAudio;

}


