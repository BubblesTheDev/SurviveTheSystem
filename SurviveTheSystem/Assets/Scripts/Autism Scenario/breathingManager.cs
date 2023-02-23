using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class breathingManager : MonoBehaviour {
    [SerializeField] private GameObject targetBox, featherIndicator;
    [SerializeField] private Image sanityBar;
    [SerializeField] private float rangeToBeOk, maxHeight;
    [SerializeField] private float moveSpeed, sanityIncrease, sanityDecrease, maxSanity;
    public float currentSanity;
    private playerHealth health;
    [SerializeField] private bool isInBox;
    float modifier = 1;

    private void Awake() {
        health = GetComponent<playerHealth>();
        featherIndicator.transform.position = new Vector3(featherIndicator.transform.position.x, 500, featherIndicator.transform.position.z);
        targetBox.transform.position = new Vector3(targetBox.transform.position.x, 500, targetBox.transform.position.z);
    }


    private void Update() {
        moveBox();
        moveFeather();
        controlSanity();
        detectDistance();

    }

    void detectDistance() {
        if (featherIndicator.transform.position.y <= targetBox.transform.position.y + 125 / 2 && featherIndicator.transform.position.y >= targetBox.transform.position.y - 125 / 2) {
            isInBox = true;
        } else isInBox = false;
    }

    void moveBox() {
        if (targetBox.transform.position.y > 500 + maxHeight && modifier == 1) {
            modifier = -1;
        } else if (targetBox.transform.position.y < 500 - maxHeight && modifier == -1) {
            modifier = 1;
        }

        targetBox.transform.Translate(Vector3.up * moveSpeed * modifier * Time.deltaTime);
    }

    void moveFeather() {
        if (Input.GetKey(KeyCode.Space)) {
            featherIndicator.transform.Translate(Vector3.up * moveSpeed * 1.75f * Time.deltaTime);
            if (featherIndicator.transform.position.y > maxHeight + 550) {
                featherIndicator.transform.position = new Vector3(featherIndicator.transform.position.x, maxHeight + 495, featherIndicator.transform.position.z);
            }
        } else {
            featherIndicator.transform.Translate(-Vector3.up * moveSpeed * 2f * Time.deltaTime);
            if (featherIndicator.transform.position.y < 450 - maxHeight) {
                featherIndicator.transform.position = new Vector3(featherIndicator.transform.position.x, 500, featherIndicator.transform.position.z);
            }
        }
    }

    void controlSanity() {
        sanityBar.fillAmount = currentSanity / maxSanity;
        if (isInBox) currentSanity += sanityIncrease * Time.deltaTime;
        else currentSanity -= sanityDecrease * Time.deltaTime;

        if (currentSanity > maxSanity) currentSanity = maxSanity;
        else if (currentSanity <= 0) {
            currentSanity = maxSanity / 2;
            StartCoroutine(health.takeDamage());
        }
    }
}
