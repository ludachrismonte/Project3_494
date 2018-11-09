using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupLevelEnum { one = 1, two = 2, three = 3, four = 4, five = 5 };

public class PickupLevel : MonoBehaviour 
{
    public PickupLevelEnum m_PickupLevel;
    
    private void Start()
    {
        GameObject cylinder = transform.Find("PickupCylinder").gameObject;
        Material cylinderMaterial = cylinder.GetComponent<Renderer>().material;
        Color cylinderColor;

        switch (m_PickupLevel)
        {
            case PickupLevelEnum.two:
                cylinderColor = Color.green;
                break;
            case PickupLevelEnum.three:
                cylinderColor = Color.blue;
                break;
            case PickupLevelEnum.four:
                cylinderColor = Color.magenta;
                break;
            case PickupLevelEnum.five:
                cylinderColor = Color.yellow;
                break;
            default:
                cylinderColor = Color.grey;
                break;
        }
        cylinderColor.a = 0.25f;
        cylinderMaterial.color = cylinderColor;
        cylinderMaterial.shader = Shader.Find("Transparent/Diffuse");

    }
}
