using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitchTouch : MonoBehaviour
{
    [SerializeField] private int sceneIndexToSwitchTo;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(sceneIndexToSwitchTo);
        }
    }
}
