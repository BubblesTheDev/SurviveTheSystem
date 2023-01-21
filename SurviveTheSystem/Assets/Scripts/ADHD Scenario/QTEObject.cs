using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTEObject : MonoBehaviour
{
    public QTEHandeler handeler;
    public float decreaseValueOffset = 1.5f;
    public float minValueToFill = 7f, maxValueToFill = 14f, decreaseValue = 2;
    public KeyCode keyToPress;
    Image fillImage;



    float valueToFill, currentValue;


    private void Awake()
    {
        handeler = GameObject.Find("GameManager").GetComponent<QTEHandeler>();
        fillImage = GetComponentInChildren<Image>();
        GetComponent<TextMeshProUGUI>().text = keyToPress.ToString();
        valueToFill = Random.Range(minValueToFill, maxValueToFill);
        decreaseValue *= Random.Range(1, decreaseValueOffset + handeler.QTESCompleted/2);
        currentValue = valueToFill / 3;
    }

    private void Update()
    {
        fillImage.fillAmount = currentValue / valueToFill;

        currentValue -= (decreaseValue * Time.deltaTime);

        if (Input.GetKeyUp(keyToPress)) currentValue++;

        if (currentValue >= valueToFill)
        {
            handeler.completeQTE(true);
            Destroy(gameObject);
        }

         if(currentValue <= 0)
        {
            handeler.completeQTE(false);
            Destroy(gameObject);
        }
    }
}
