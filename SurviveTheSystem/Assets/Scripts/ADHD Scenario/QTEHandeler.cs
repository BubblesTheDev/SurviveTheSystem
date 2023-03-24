using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QTEHandeler : MonoBehaviour
{
    public int QTESCompleted;
    public GameObject QTEPrefab;
    public List<KeyCode> keysForQTE;
    public List<Sprite> backgroundsForQTE;
    GameObject currentQTE;
    public float maxTimeBetweenQTE, minTimeBetweenQTE;
    public bool canSpawnQTE;
    public playerHealth health;

    public TextMeshProUGUI QTECountTex;


    private void Awake()
    {
        spawnQTE();
        QTECountTex.text = "Youve Stopped yourself from stimming " + QTESCompleted + " times! Good Job at being a good student and hiding your neruodivergency!";

    }

    private void Update()
    {
        if (canSpawnQTE) spawnQTE();
    }

    public void completeQTE(bool completed)
    {
        if (completed)
        {
            QTESCompleted++;
            QTECountTex.text = "Youve Stopped yourself from stimming " + QTESCompleted + " times! Good Job at being a good student and hiding your neruodivergency!";
            StartCoroutine(QTETimer());
        }
        else
        {
            StartCoroutine(QTETimer());
            StartCoroutine(health.takeDamage());
        }
    }

    public IEnumerator QTETimer()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBetweenQTE, maxTimeBetweenQTE));
        canSpawnQTE = true;
    }

    public void spawnQTE()
    {
        canSpawnQTE = false;
        Vector2 posToSpawnQTE = new Vector2(Random.Range(125, 1800), Random.Range(250, 840));
        currentQTE = Instantiate(QTEPrefab, posToSpawnQTE, Quaternion.identity, GameObject.Find("QTE Holder").transform);

        KeyCode temp = keysForQTE[Random.Range(0, keysForQTE.Count)];

        currentQTE.GetComponent<QTEObject>().keyToPress = temp;
        currentQTE.GetComponent<Text>().text = temp.ToString();
        currentQTE.GetComponentInChildren<Image>().sprite = backgroundsForQTE[Random.Range(0, backgroundsForQTE.Count)];
        currentQTE.transform.localScale = Vector3.one * Random.Range(.75f, 1.5f);
        
    }
}
