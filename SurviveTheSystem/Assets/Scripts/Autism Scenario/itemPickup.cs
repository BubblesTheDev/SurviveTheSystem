using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class itemPickup : MonoBehaviour {
    public float timeToPickupItem;
    [SerializeField] float rayCastThickness;
    [SerializeField] private float pickupRange;
    public LayerMask itemMask;
    private itemManager manager;
    public Image selectTimerImage;
    private float currentTime;


    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<itemManager>();
    }

    public void Update() {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) {
            RaycastHit hit;
            Physics.SphereCast(transform.position, rayCastThickness, transform.forward, out hit, pickupRange, itemMask.value);
            if (hit.transform != null) {
                print(hit.transform.gameObject.name);
                currentTime += Time.deltaTime;
                if (currentTime > timeToPickupItem) {
                    if (manager.itemsToPickup.Contains(hit.transform.gameObject)) {
                        manager.removeItem(hit.transform.gameObject);
                        currentTime = 0;
                    }
                }
            }
        } else {
            currentTime = 0;
        }

        selectTimerImage.fillAmount = currentTime / timeToPickupItem;
    }
}
