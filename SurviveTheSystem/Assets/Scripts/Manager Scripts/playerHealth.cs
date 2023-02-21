using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{

    [SerializeField] private bool deathToMenu;
    [SerializeField] private int maxHearts = 3;
    public int currentHearts;
    [SerializeField] private GameObject[] healthImage;
    [SerializeField] private Image redDamageThing;

    [SerializeField] private AudioSource heartBeat;
    [SerializeField] private AudioClip[] heartbeatSpeeds;


    private void Update()
    {
        if (currentHearts <= 0 && deathToMenu)  SceneManager.LoadScene(0);
        
    }


    public IEnumerator takeDamage() 
    {
        redDamageThing.enabled = true;
        yield return new WaitForSeconds(.1f);
        redDamageThing.enabled = false;
        if(healthImage.Length > 1) Destroy(healthImage[currentHearts - 1]);

        currentHearts--;
        controlHeartbeat();
    }

    public void controlHeartbeat()
    {
        switch (currentHearts)
        {
            case 3:
            heartBeat.clip = heartbeatSpeeds[2];
                break;
            case 2:

            heartBeat.clip = heartbeatSpeeds[1];
                break;
            case 1:

            heartBeat.clip = heartbeatSpeeds[0];
                break;
        }

        heartBeat.Play();
    }

}
