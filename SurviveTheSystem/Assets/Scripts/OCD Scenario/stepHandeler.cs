using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepHandeler : MonoBehaviour
{
    public GameObject leftLeg, RightLeg;
    public bool currentLegRight;
    public float legMoveSpeed;
    public float maxLegDistance;
    int ammountOfSteps;
    [HideInInspector]
    public GameObject objectToMove, otherLeg;
    [HideInInspector]
    public bool startMoving;
    bool canDrop = true;
    public playerHealth health;

    public GameObject[] plates;
    public GameObject[] legKeys;

    public GameObject cameraHolder;
    private void Awake()
    {
        spawnPlates();
    }

    private void Update()
    {
        moveLeg();

        if (Input.GetKeyDown(KeyCode.D) && currentLegRight)
        {
            startMoving = true;
            objectToMove = RightLeg;
            otherLeg = leftLeg;
        }
        if (Input.GetKeyDown(KeyCode.A) && !currentLegRight)
        {
            startMoving = true;
            objectToMove = leftLeg;
            otherLeg = RightLeg;
        }

        if (Input.GetKeyUp(KeyCode.A) && startMoving && !currentLegRight) dropLeg();
        if (Input.GetKeyUp(KeyCode.D) && startMoving && currentLegRight) dropLeg();

        cameraHolder.transform.position = new Vector3(cameraHolder.transform.position.x,cameraHolder.transform.position.y, (leftLeg.transform.position.z + RightLeg.transform.position.z)/2);

    }

    public void moveLeg()
    {
        if (startMoving)
        {
            objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, Vector3.up.y * .5f, objectToMove.transform.position.z);
            objectToMove.transform.position += Vector3.forward * legMoveSpeed * Time.deltaTime;

            if (Vector3.Distance(objectToMove.transform.position, otherLeg.transform.position) <= 1f) canDrop = true;
        }

        if(objectToMove != null && otherLeg != null)
        {
            if (Vector3.Distance(objectToMove.transform.position, otherLeg.transform.position) >= maxLegDistance && canDrop) dropLeg();
        }

    }

    void dropLeg()
    {
        startMoving = false;
        canDrop = false; 
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, 0, objectToMove.transform.position.z);

        currentLegRight = !currentLegRight;
    }

    public void increaseStep(bool steppedOnCrack)
    {
        if (steppedOnCrack) StartCoroutine(health.takeDamage());
    }

    void spawnPlates()
    {
        for (int i = 0; i < 25; i++)
        {
            Instantiate(plates[Random.Range(0, plates.Length)], transform.position + (Vector3.forward * 2.1f) * (i+1), Quaternion.identity, GameObject.Find("Plate Holder").transform);
        }
    }
}
