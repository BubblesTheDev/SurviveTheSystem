using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class teacherHandler : MonoBehaviour
{
    public List<teacherActions> actions;
    public rotateBackAndForthVertical rotateScript;
    public bool isDoingAction;
    private int index;
    public float minTimeActions, maxTimeActions;
    
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rotateScript = GetComponent<rotateBackAndForthVertical>();
        index = (int)Random.Range(0, actions.Count);
        soundManager.playClip("lesson");
    }


    private void Update()
    {
        if (!isDoingAction) StartCoroutine(doAction());
    }

    public IEnumerator doAction()
    {
        isDoingAction = true;

        agent.SetDestination(actions[index].referenceToEnd.transform.position);
        while (agent.remainingDistance < 0.5f)
        {
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = Quaternion.LookRotation(actions[index].referenceToEnd.transform.rotation.eulerAngles);
        if (actions[index].actionSound != "") soundManager.playClip(actions[index].actionSound);
        if (actions[index].rotate)
        {
            print("start rotating");
            rotateScript.enabled = true;
            yield return new WaitForSeconds(actions[index].rotateDuration);
            rotateScript.enabled = false;
            transform.rotation = Quaternion.LookRotation(actions[index].referenceToEnd.transform.rotation.eulerAngles);
            print("stop rotating");
        }

            print("wait time");
        float timeToWait = Random.Range(minTimeActions, maxTimeActions);
        yield return new WaitForSeconds(timeToWait);
        index = (int)Random.Range(0, actions.Count);
        isDoingAction = false;
    }

}

[System.Serializable]
public struct teacherActions
{
    public GameObject referenceToEnd;
    public bool rotate;
    public float rotateDuration;
    public string actionSound;
}
