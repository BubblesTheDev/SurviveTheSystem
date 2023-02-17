using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    public List<GameObject> itemsToPickup;


    public void removeItem(GameObject itemToRemove)
    {
        print(itemToRemove.name + " has been picked up!");
        itemsToPickup.Remove(itemToRemove);
        Destroy(itemToRemove);
    }
}
