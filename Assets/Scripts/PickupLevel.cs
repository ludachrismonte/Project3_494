using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupLevelEnum { one = 1, two = 2, three = 3, four = 4, five = 5 };

public class PickupLevel : MonoBehaviour 
{
    public PickupLevelEnum m_PickupLevel;

    private void Start()
    {
        Material newMaterial;
        switch (m_PickupLevel)
        {
            case PickupLevelEnum.two:
                newMaterial = Resources.Load("PickupParticlesLevel2", typeof(Material)) as Material;
                break;
            case PickupLevelEnum.three:
                newMaterial = Resources.Load("PickupParticlesLevel3", typeof(Material)) as Material;
                break;
            case PickupLevelEnum.four:
                newMaterial = Resources.Load("PickupParticlesLevel4", typeof(Material)) as Material;
                break;
            case PickupLevelEnum.five:
                newMaterial = Resources.Load("PickupParticlesLevel5", typeof(Material)) as Material;
                break;
            default:
                newMaterial = Resources.Load("PickupParticlesLevel1", typeof(Material)) as Material;
                break;
        }

        if (newMaterial != null)
        {
            gameObject.GetComponent<ParticleSystemRenderer>().material = newMaterial;
        }
    }
}
