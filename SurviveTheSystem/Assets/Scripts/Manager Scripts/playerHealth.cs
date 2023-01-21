using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;
    public GameObject[] healthImage;
    public Image redDamageThing;

    private void Update()
    {
        if (currentHearts <= 0) SceneManager.LoadScene(0);
    }


    public IEnumerator takeDamage() 
    {
        redDamageThing.enabled = true;
        yield return new WaitForSeconds(.1f);
        redDamageThing.enabled = false;
        if(healthImage.Length > 1) Destroy(healthImage[currentHearts - 1]);

        currentHearts--;
    }

}
