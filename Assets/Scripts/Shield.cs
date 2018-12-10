using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private Vector3 initial;
    private float multiplier;
    private float timer;
    public float duration = 10f;
    public float speed = 10f;
    private int activeShields;  // Keep track of how many shields are currently active

    public GameObject myCollider;

    // Use this for initialization
    void Start()
    {
        myCollider.SetActive(false);
        timer = duration;
        initial = gameObject.transform.localScale;
        gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
        if (timer < duration)
        {
            timer += Time.deltaTime;
            multiplier = Mathf.Sin(timer);
            multiplier = (multiplier / 12) + 1;
            gameObject.transform.localScale = initial * multiplier;
        }
    }

    public void Activate()
    {
        myCollider.SetActive(true);
        StartCoroutine(Run());
    }

    public void Deactivate()
    {
        myCollider.SetActive(false);
        gameObject.transform.localScale = Vector3.zero;
    }

    private IEnumerator Run()
    {
        activeShields++;

        if (activeShields == 1)
        {
            for (float i = 0; i < 1f; i += Time.deltaTime)
            {
                gameObject.transform.localScale = initial * i;
                yield return null;
            }
        }

        timer = 0f;
        yield return new WaitForSeconds(duration);

        if (activeShields == 1)
        {
            myCollider.SetActive(false);
            Vector3 current = gameObject.transform.localScale;
            for (float i = 1; i > 0f; i -= Time.deltaTime)
            {
                gameObject.transform.localScale = current * i;
                yield return null;
            }
        }
        activeShields--;
    }

}
