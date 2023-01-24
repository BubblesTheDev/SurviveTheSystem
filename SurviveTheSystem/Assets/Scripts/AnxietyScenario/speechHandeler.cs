using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speechHandeler : MonoBehaviour
{

    [Header("Speech Settings")]
    public GameObject choiceSpawningBasePos;
    public float choiceSize;
    public Bounds spawnBounds;
    public GameObject[] possibleGoodChoices, possibleBadChoices;
    public int maxChoicesPerBurst;
    public int maxGoodChoices, maxBadChoices;
    int currentGoodChoices, currentBadChoices;
    public float timePerBurst;
    public bool hasSpawned;
    public GameObject[] brainfog;

    [HideInInspector]
    public int choicesMade;
    public int choicesToMake;
    public List<wordChoices> speechList;

    [Space, Header("Choosing Settings")]
    public GameObject cameraObj;
    public float rayCastThickness;
    public LayerMask layersToHit;
    public float timeToChoose;
    float currentTime;
    RaycastHit hit;
    public Image choosingBar;
    private playerHealth health;

    private void Awake()
    {
        health = GetComponent<playerHealth>();
        spawnChoices();
    }


    private void Update()
    {
        chooseObject();
        choosingBar.fillAmount = currentTime / timeToChoose;
    }
    void spawnChoices()
    {
        hasSpawned = true;
        for (int i = 0; i < maxChoicesPerBurst;)
        {
            Vector3 spawnPoint = getRandomPointInBounds(spawnBounds);
            if(!Physics.CheckSphere(spawnPoint, choiceSize))
            {
                if(currentGoodChoices < maxGoodChoices)
                {
                    Instantiate(possibleGoodChoices[Random.Range(0, possibleGoodChoices.Length)], spawnPoint, Quaternion.identity, transform.Find("Choice Holder"));
                    currentGoodChoices++;
                    i++;
                }
                else if (currentBadChoices < maxBadChoices)
                {
                    Instantiate(possibleBadChoices[Random.Range(0, possibleBadChoices.Length)], spawnPoint, Quaternion.identity, transform.Find("Choice Holder"));
                    currentBadChoices++;
                    i++;
                }
            }
        }

        currentGoodChoices = 0;
        currentBadChoices = 0;
        
    }


    public Vector3 getRandomPointInBounds(Bounds bound)
    {
        float minX = bound.size.x * -0.5f;
        float minY = bound.size.y * -0.5f;
        float minZ = bound.size.z * -0.5f;

        return (Vector3) choiceSpawningBasePos.transform.TransformPoint(
        new Vector3(Random.Range(minX, -minX),
            Random.Range(minY, -minY),
            Random.Range(minZ, -minZ))
    );
    }

    public void choiceHasBeenMade(bool isGoodChoice, GameObject objChosen)
    {

        if (!isGoodChoice)
        {
            StartCoroutine(health.takeDamage());
            int x = Random.Range(3, 5);
            for (int i = 0; i < x; i++)
            {
                Vector2 spawnPoint = new Vector2(Random.Range(125, Screen.width - 350), Random.Range(125, Screen.height-350));
                GameObject temp = Instantiate(brainfog[Random.Range(0, brainfog.Length)], spawnPoint, Quaternion.identity, GameObject.Find("Brain Fog Holder").transform);
                temp.transform.localScale = temp.transform.localScale * Random.Range(0.25f, 1f);

            }
        }
        if (choicesMade < choicesToMake)
        {
            //speechList.Add(objChosen.transform.name);
            hasSpawned = false;
            choicesMade++;
        }

        foreach (Transform item in transform.Find("Choice Holder"))
        {
            Destroy(item.gameObject);
            
        }

        if (!hasSpawned) spawnChoices();

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
public struct wordChoices {
    public string speechPart;
    public List<string> goodChoices;
    public List<string> badChoice;
}
