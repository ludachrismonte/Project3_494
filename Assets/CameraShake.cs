using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 axisShakeMin;
    public Vector3 axisShakeMax;
    public float timeOfShake;
    private float timeOfShakeStore;
    private bool shake;
    private Vector3 startPos;
    // Use this for initialization
    void Start()
    {
        shake = false;
        startPos = transform.position;
        timeOfShakeStore = timeOfShake;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
        {
            transform.position = startPos + new Vector3(Random.Range(axisShakeMin.x, axisShakeMax.x), Random.Range(axisShakeMin.y, axisShakeMax.y), Random.Range(axisShakeMin.z, axisShakeMax.z));
            timeOfShake -= Time.deltaTime;
            if (timeOfShake <= 0.0f)
            {
                shake = false;
                transform.position = startPos;
            }
        }
    }
    public void ShakeCamera(float shakeTime = -1.0f)
    {
        if (shakeTime > 0.0f)
        {
            timeOfShake = shakeTime;
        }
        else
        {
            timeOfShake = timeOfShakeStore;
        }
        shake = true;
    }
}