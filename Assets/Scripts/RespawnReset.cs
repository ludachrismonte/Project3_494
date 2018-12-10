using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnReset : MonoBehaviour
{
    public float waitTime = 10;
    public float checkReset = 0.2f;
    public GameObject cameraParent;
    public GameObject cameraMain;
    Rigidbody carRb;
    bool checkingStuck = false;
    public bool stuck = false;

    private float timer = 0f;

    public struct RespawnStruct
    {
        public Vector3 respawnPos;
        public Quaternion respawnRotate;
        public Vector3 respawnCameraMain;
        public Quaternion cameraRotateMain;
        public Vector3 respawnCameraParent;
        public Quaternion cameraRotateParent;
        public RespawnStruct(Vector3 carPos, Vector3 mainCamPos, Vector3 camParentPos, Quaternion carRotate, Quaternion mainCamRotate, Quaternion camParentRotate)
        {
            respawnPos = carPos;
            respawnCameraMain = mainCamPos;
            respawnCameraParent = camParentPos;
            respawnRotate = carRotate;
            cameraRotateMain = mainCamRotate;
            cameraRotateParent = camParentRotate;
        }
        public void Set(Vector3 carPos, Vector3 mainCamPos, Vector3 camParentPos, Quaternion carRotate, Quaternion mainCamRotate, Quaternion camParentRotate)
        {
            respawnPos = carPos;
            respawnCameraMain = mainCamPos;
            respawnCameraParent = camParentPos;
            respawnRotate = carRotate;
            cameraRotateMain = mainCamRotate;
            cameraRotateParent = camParentRotate;
        }
    }

    RespawnStruct respawnStruct;
    RespawnStruct resetStruct;
    RespawnStruct resetStruct2;

    void Awake()
    {
        carRb = GetComponent<Rigidbody>();
        respawnStruct = new RespawnStruct(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
        resetStruct = new RespawnStruct(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
        resetStruct2 = new RespawnStruct(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
        StartCoroutine(FindResetPoint());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 20) {
            return;
        }
        if (stuck && Input.GetKey(KeyCode.Z))
        {
            ResetCar();
            return;
        }
        CheckifStuck();
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 15 && stuck)
        {
            stuck = false;
        }
    }

    private void CheckifStuck()
    {
        if (!checkingStuck && gameObject.GetComponent<ControllerInput>().enabled)
        {
            StartCoroutine(CheckifStuckHelper());
        }
    }

    public void ResetCar()
    {
        stuck = false;
        StartCoroutine(ResetEnum());
    }

    public void Respawn(int i)
    {
        StartCoroutine(RespawnEnum(i));
    }

    void RespawnHelper()
    {
        cameraParent.gameObject.transform.position = respawnStruct.respawnCameraMain;
        cameraParent.gameObject.transform.rotation = respawnStruct.cameraRotateParent;
        cameraParent.GetComponent<CameraController>().enabled = false;
        transform.position = respawnStruct.respawnPos;
        transform.rotation = respawnStruct.respawnRotate;
    }

    IEnumerator RespawnEnum(int i)
    {
        //if i ==1 this is a reset
        gameObject.GetComponent<ControllerInput>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        RespawnHelper();
        cameraParent.GetComponent<CameraController>().enabled = true;
        this.transform.Find("SkyCar").gameObject.SetActive(true);
        if (i == 0) { gameObject.GetComponent<PlayerPickup>().Respawn(); }
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<ControllerInput>().enabled = true;
        checkingStuck = false;
        stuck = false;
    }

    IEnumerator CheckifStuckHelper()
    {
        checkingStuck = true;

        Vector3 pos1 = transform.position;
        float vel = carRb.velocity.magnitude;
        yield return new WaitForSeconds((int)waitTime / 2);
        float vel2 = carRb.velocity.magnitude;
        Vector3 pos2 = transform.position;
        yield return new WaitForSeconds((int)waitTime / 2);
        if ((int)vel == (int)carRb.velocity.magnitude && (int)vel == 0 && (int)vel == (int)vel2)
        {
            stuck = Check(pos1, pos2, transform.position);        
        }

        checkingStuck = false;
    }

    private IEnumerator FindResetPoint()
    {
        while(true)
        {
            yield return new WaitForSeconds(checkReset);
            if (carRb.velocity.magnitude > 7 && Mathf.Abs(carRb.velocity.x) > 2 && Mathf.Abs(carRb.velocity.z) > 2 && !stuck)
            {
                resetStruct2 = resetStruct;
                resetStruct.Set(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
            }
        }       
    }

    private bool Check(Vector3 pos1,Vector3 pos2,Vector3 pos3)
    {
        bool xStationary = (int)pos1.x == (int)pos2.x && (int)pos1.x == (int)pos3.x && (int)pos3.x == (int)pos2.x;
        bool zStationary = (int)pos1.z == (int)pos2.z && (int)pos1.z == (int)pos3.z && (int)pos3.z == (int)pos2.z;
        return xStationary && zStationary;
    }

    private IEnumerator ResetEnum()
    {
        for (int i = 0; i < 4; i++)
        {
            transform.Find("SkyCar").gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
            transform.Find("SkyCar").gameObject.SetActive(false);
            yield return new WaitForSeconds(.1f);
        }
        resetStruct.Set(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
        RespawnStruct temp = respawnStruct;
        respawnStruct = resetStruct2;
        Respawn(1);
        respawnStruct = temp;
        resetStruct = temp;
        resetStruct2 = temp;
    }
}

