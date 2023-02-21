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
    public Sprite closeSprite;
    public Sprite defaultSprite;


    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<itemManager>();
    }

    public void Update() {
        RaycastHit hitSphere;
        RaycastHit hitRay;
        Physics.SphereCast(transform.position, rayCastThickness, transform.forward, out hitSphere, pickupRange, itemMask.value);
        Physics.Raycast(transform.position, transform.forward, out hitRay, Mathf.Infinity, adMask.value);

        if (hitSphere.transform != null && hitSphere.transform.gameObject.CompareTag("Item")) indicatorImage.sprite = handSprite;
        else if (hitRay.transform != null && hitRay.transform.gameObject.CompareTag("AdObject")) indicatorImage.sprite = closeSprite;
        else indicatorImage.sprite = defaultSprite;

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) {
            
            if (hitSphere.transform != null && hitSphere.transform.gameObject.CompareTag("Item")) {
                currentTime += Time.deltaTime;
                if (currentTime > timeToPickupItem) {
                    if (manager.itemsToPickup.Contains(hitSphere.transform.gameObject)) {
                        manager.removeItem(hitSphere.transform.gameObject);
                        currentTime = 0;
                    }
                }
            }
            if (hitRay.transform != null && hitRay.transform.gameObject.CompareTag("AdObject"))
            {
                currentTime += Time.deltaTime;
                if (currentTime > timeToPickupItem)
                {
                    hitRay.transform.gameObject.GetComponentInParent<adObject>().closeAd();
                    currentTime = 0;
                }
            }
        } else {
            currentTime = 0;
        }

        selectTimerImage.fillAmount = currentTime / timeToPickupItem;
    }
}
