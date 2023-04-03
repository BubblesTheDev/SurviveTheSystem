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



    private void Awake()
    {
        spawnQTE();

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
        currentQTE.transform.Find("Text").GetComponent<Text>().text = temp.ToString();
        currentQTE.transform.Find("Background").GetComponent<Image>().sprite = backgroundsForQTE[Random.Range(0, backgroundsForQTE.Count)];
        currentQTE.transform.localScale = Vector3.one * Random.Range(.75f, 1.5f);
        
    }
}
