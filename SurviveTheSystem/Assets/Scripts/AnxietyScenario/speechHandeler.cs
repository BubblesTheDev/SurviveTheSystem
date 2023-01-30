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
    public float maxTimeForChoice; 
    public bool hasSpawned;
    public GameObject[] brainfog;
    public GameObject choicePrefab;
    private bool audioHasFinished;
    
    

    [Space, Header("Choosing Settings")]
    public GameObject cameraObj;
    public float rayCastThickness;
    public LayerMask layersToHit;
    public float timeToChoose;
    float currentTime;
    RaycastHit hit;
    public Image choosingBar;
    private playerHealth health;

    public AudioSource audioPlayer;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        health = GetComponent<playerHealth>();
    }


    private void Update()
    {
        chooseObject();
        choosingBar.fillAmount = currentTime / timeToChoose;
    }
    public void spawnChoices()
    {
        hasSpawned = true;

        int randomGoodChoiceAmmount = Random.Range(2, speechList[speechPartIndex].goodChoices.Count);
        int randomBadChoiceAmmount = Random.Range(3, speechList[speechPartIndex].badChoices.Count);

        for (int i = 0; i < speechList[speechPartIndex].goodChoices.Count;)
        {
            Vector3 spawnPoint = getRandomPointInBounds(spawnBounds);
            if (!Physics.CheckSphere(spawnPoint, choiceSize))
            {
                GameObject temp = Instantiate(choicePrefab, spawnPoint, Quaternion.identity, transform.Find("Text Canvas"));

                temp.GetComponent<TextMeshPro>().text = speechList[speechPartIndex].goodChoices[i].choiceText;
                temp.GetComponent<choiceCounter>().isGood = true;
                i++;
            }
        }

        for (int i = 0; i < speechList[speechPartIndex].badChoices.Count; i++)
        {
            Vector3 spawnPoint = getRandomPointInBounds(spawnBounds);
            if (!Physics.CheckSphere(spawnPoint, choiceSize))
            {
                GameObject temp = Instantiate(choicePrefab, spawnPoint, Quaternion.identity, transform.Find("Text Canvas"));

                temp.GetComponent<TextMeshPro>().text = speechList[speechPartIndex].badChoices[i].choiceText;
                temp.GetComponent<choiceCounter>().isGood = false;
                i++;
            }
        }

        hasSpawned = false;

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

    public void choiceHasBeenMade(bool isGoodChoice, GameObject objChosen)
    {

        if (!isGoodChoice)
        {
            takeDamage();
        }

        foreach (Transform item in transform.Find("Choice Holder"))
        {
            Destroy(item.gameObject);

        }

        if (!hasSpawned) spawnChoices();

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

        Physics.SphereCast(cameraObj.transform.position, rayCastThickness, cameraObj.transform.forward, out hit, Mathf.Infinity, layersToHit.value);
        if (hit.transform != null)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeToChoose)
            {
                choiceHasBeenMade(hit.transform.gameObject.GetComponent<choiceCounter>().isGood, hit.transform.gameObject);
            }
        }
        else currentTime = 0;
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


