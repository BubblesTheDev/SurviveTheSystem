using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class experienceBase : MonoBehaviour
{
    private void Awake()
    {
        experienceHandler.current.onExpereienceCall += playExperience;
    }

    private void playExperience(string obj)
    {
        throw new NotImplementedException();
    }
}
