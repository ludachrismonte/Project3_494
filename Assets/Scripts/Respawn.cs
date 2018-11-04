using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //public Vector3 repawnPos;
    //public object respawnPos { get; private set; }
    private Vector3 respawnPos;
    private Quaternion respawnRotate;
    // Use this for initialization
    void Start()
    {
        respawnPos = transform.position;
        respawnRotate = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeRespawn(Vector3 respawn)
    {
        respawnPos = respawn;
    }

    public void respawn()
    {
        StartCoroutine(RespawnEnum());
    }

    IEnumerator RespawnEnum(){
        gameObject.GetComponent<ControllerInput>().enabled = false;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = respawnRotate;
        transform.position = respawnPos;
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<ControllerInput>().enabled = true;
    }
}
