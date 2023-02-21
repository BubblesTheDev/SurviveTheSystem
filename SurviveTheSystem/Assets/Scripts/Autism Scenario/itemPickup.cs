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
    public LayerMask adMask;
    private itemManager manager;
    public Image selectTimerImage;
    private float currentTime;
    public Image indicatorImage;
    public Sprite handSprite;
    public Sprite defaultSprite;


    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<itemManager>();
    }

    public void Update() {
        RaycastHit hit;
        Physics.SphereCast(transform.position, rayCastThickness, transform.forward, out hit, pickupRange, itemMask.value);

        if (hit.transform != null) indicatorImage.sprite = handSprite;
        else indicatorImage.sprite = defaultSprite;

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) {
            Physics.SphereCast(transform.position, rayCastThickness, transform.forward, out hit, pickupRange, itemMask.value);
            if (hit.transform != null && hit.transform.gameObject.CompareTag("Item")) {
                currentTime += Time.deltaTime;
                if (currentTime > timeToPickupItem) {
                    if (manager.itemsToPickup.Contains(hit.transform.gameObject)) {
                        manager.removeItem(hit.transform.gameObject);
                        currentTime = 0;
                    }
                }
            }
            Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, adMask.value);
            if (hit.transform != null && hit.transform.gameObject.CompareTag("Ad"))
            {
                currentTime += Time.deltaTime;
                if (currentTime > timeToPickupItem)
                {
                    GetComponentInParent<adObject>().closeAd();
                }
            }
        } else {
            currentTime = 0;
        }

        selectTimerImage.fillAmount = currentTime / timeToPickupItem;
    }
}
