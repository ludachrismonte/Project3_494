﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour 
{
    void Start()
    {
        Screen.SetResolution(16 * 100, 10 * 100, true);
    }
}