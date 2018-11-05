using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour 
{
    public GameObject target;

    void LateUpdate()
    {
        if (target != null && target.activeSelf)
            transform.LookAt(target.transform);
    }
}

