using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneCallExperience : experienceBase
{
    [SerializeField] private GameObject[] peopleToChooseFrom;
    public bool isActive;

    public override void playExperience(string obj)
    {
        isActive = true;
        soundManager.playClip(obj);
        GameObject.Find(obj).transform.position = peopleToChooseFrom[Random.Range(0, peopleToChooseFrom.Length)].transform.position;
        GameObject.Find(obj).GetComponent<AudioSource>().maxDistance = 2.5f;
        StartCoroutine(phoneCallAnim());
    }

    public IEnumerator phoneCallAnim()
    {
        GameObject temp = peopleToChooseFrom[Random.Range(0, peopleToChooseFrom.Length)];
        temp.GetComponent<rotateBackAndForthVertical>().enabled = true;

        yield return new WaitForSeconds(3);

        temp.GetComponent<rotateBackAndForthVertical>().enabled = false;
        temp.transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }
}
