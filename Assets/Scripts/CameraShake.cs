using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;
    
    // How long the object should shake for.
    public float shakeDuration = 0f;
    
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public bool start = false;
    
    Vector3 originalPos;
    
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }
    
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
    
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeAmount *= 0.8f;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
        if (start){
            Shake(0.7f, 2.0f);
            start = false;
        }
    }

    public void Shake(float amount, float duration){

        // Don't cancel out bigger shakes with smaller ones
        if (amount >= shakeAmount)
        {
            shakeAmount = amount;
            shakeDuration = duration;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        string target = collision.gameObject.tag;
        if ( (target == "Player") || (target == "Player2") || (target == "Player3") || (target == "Player4") ){
            float intensity = collision.relativeVelocity.magnitude / 25.0f;
            Shake(intensity, 2.0f);
            collision.gameObject.GetComponent<CameraShake>().Shake(intensity, 2.0f);
        }
    }

}
