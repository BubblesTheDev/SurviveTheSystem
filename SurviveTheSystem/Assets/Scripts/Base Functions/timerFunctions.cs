using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class timerFunctions
{

    public static bool countDownTImer(float timeToCountDown)
    {
        if (timeToCountDown > 0)
        {
            timeToCountDown -= Time.deltaTime;
            return false;
        }
        else return true;
    }

    
}
