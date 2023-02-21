using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject leaveStoreIndicator;

    private void Awake()
    {
        state = stageState.pickupItems;
    }

    private void Update()
    {
        if (state == stageState.checkoutItems && !hasStartedCheckout && Vector3.Distance(playerObj.transform.position, checkout.transform.position) > distanceToCheckout) StartCoroutine(detectCheckout());
    }

    public void removeItem(GameObject itemToRemove)
    {
        print(itemToRemove.name + " has been picked up!");
        itemsToPickup.Remove(itemToRemove);
        pickupSound.Play();
        Destroy(itemToRemove);
        if (itemsToPickup.Count == 0) state = stageState.checkoutItems;
    }

    public IEnumerator detectCheckout()
    {
        playerObj.GetComponent<CharacterMovement>().canMove = false;
        for (int i = 0; i < itemsToPickup.Count; i++)
        {
            yield return new WaitForSeconds(checkoutWaitTime / itemsToPickup.Count);
            checkoutSource.Play();
        }

        state = stageState.leaveStore;
    }
}

public enum stageState
{
    pickupItems,
    checkoutItems,
    leaveStore
}