using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{

    public bool deathToMenu;
    public int maxHearts = 3;
    public int currentHearts;
    public GameObject[] healthImage;
    public Image redDamageThing;

    public AudioSource heartBeat;

    private void Update()
    {
        if (currentHearts <= 0 && deathToMenu)  SceneManager.LoadScene(0);
        controlHeartbeat();
    }


    public IEnumerator takeDamage() 
    {
        redDamageThing.enabled = true;
        yield return new WaitForSeconds(.1f);
        redDamageThing.enabled = false;
        if(healthImage.Length > 1) Destroy(healthImage[currentHearts - 1]);

        currentHearts--;
    }

    public void controlHeartbeat()
    {
        switch (currentHearts)
        {
            case 3:
                heartBeat.volume = .33f;
                break;
            case 2:
                heartBeat.volume = .66f;

                break;
            case 1:
                heartBeat.volume = 1f;

                break;
        }
    }

}
