using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemManager : MonoBehaviour
{
    public stageState state;

    [Header("PickingUpItems")]
    public List<GameObject> itemsToPickup;
    public AudioSource pickupSound;

    [Header("Checkout")]
    public GameObject checkout;
    public GameObject checkoutInidcator;
    public float distanceToCheckout;
    public float checkoutWaitTime;
    public bool hasStartedCheckout;
    public GameObject playerObj;
    public AudioSource checkoutSource;

    [Header("LeaveStore")]
    public GameObject doorToSpawn;
    public GameObject doorPosition;
    bool doorIsSpawned;

    [Header("Hud Stuff")]
    public TextMeshProUGUI objectiveText;

    private void Awake()
    {
        state = stageState.pickupItems;
    }

    private void Update()
    {
        if (state == stageState.checkoutItems && !hasStartedCheckout && Vector3.Distance(playerObj.transform.position, checkout.transform.position) < distanceToCheckout) StartCoroutine(detectCheckout());
        leaveStore();
        updateHud();

        if (Input.GetKeyDown(KeyCode.F2)) {
            state = stageState.checkoutItems;
        }
    }

    public void updateHud() {
        if(state == stageState.pickupItems) {
            objectiveText.text = ("- Pickup Jam \n- Pickup Pizza \n- Pickup Juice \n- Pickup Milk");
        } else if( state == stageState.checkoutItems) {
            objectiveText.text = ("Head to the open \n Checkout lane");
        } else if(state == stageState.leaveStore) {
            objectiveText.text = ("You can leave the store now, the door has spawned");
        }
    }

    public void removeItem(GameObject itemToRemove) {
        print(itemToRemove.name + " has been picked up!");
        itemsToPickup.Remove(itemToRemove);
        pickupSound.Play();
        Destroy(itemToRemove);
        if (itemsToPickup.Count == 0) {
            state = stageState.checkoutItems;
            checkoutInidcator.SetActive(true);
        }
    }

    public IEnumerator detectCheckout()
    {
        playerObj.GetComponent<CharacterMovement>().canMove = false;
        hasStartedCheckout = true;
        for (int i = 0; i < itemsToPickup.Count; i++)
        {
            checkoutSource.Play();
            yield return new WaitForSeconds(checkoutSource.clip.length);
            yield return new WaitForSeconds(checkoutWaitTime / itemsToPickup.Count);
            
        }

        state = stageState.leaveStore;
        playerObj.GetComponent<CharacterMovement>().canMove = true;

    }

    void leaveStore() {
        if (state == stageState.leaveStore && !doorIsSpawned) {
            Instantiate(doorToSpawn, doorPosition.transform.position, doorPosition.transform.rotation, GameObject.Find("Props").transform);
            doorIsSpawned = true;
        }
    }
}

public enum stageState
{
    pickupItems,
    checkoutItems,
    leaveStore
}