using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class itemPickup : MonoBehaviour
{
    public float timeToPickupItem;
    [SerializeField] float rayCastThickness;
    public LayerMask itemMask;
    private itemManager manager;
    public Image selectTimerImage;
    private float currentTime;
    

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<itemManager>();
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit hit;
            Physics.SphereCast(transform.position, rayCastThickness, transform.forward, out hit, Mathf.Infinity, itemMask.value);
            if (hit.transform != null)
            {
                currentTime += Time.deltaTime;
                if (currentTime > timeToPickupItem)
                {
                    if (manager.itemIdsToPickup.Contains(hit.transform.gameObject.GetComponent<itemData>().itemId))
                    {
                        manager.removeItem(hit.transform.gameObject.GetComponent<itemData>().itemId);
                    }
                }
            }
        } 
        else
        {
            currentTime = 0;
        }
    }
}
