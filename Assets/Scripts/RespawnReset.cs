using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnReset : MonoBehaviour
{
    //public Vector3 repawnPos;
    //public object respawnPos { get; private set; }
    public float waitTime = 10;
    public GameObject cameraParent;
    public GameObject cameraMain;
    Rigidbody carRb;
    bool checkingStuck = false;
    public bool stuck = false;

    // Use this for initialization

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
        //public void Respawn(){
        //    this.transform.position = respawnPos;
        //    respawnCameraMain = mainCamPos;
        //    respawnCameraParent = camParentPos;
        //    respawnRotate = carRotate;
        //    cameraRotateMain = mainCamRotate;
        //    cameraRotateParent = camParentRotate;
        //}
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

    }

    // Update is called once per frame
    void Update()
    {
        //if (stuck && Input.GetKey(KeyCode.Z))
        //{
        //    ResetCar();
        //    return;
        //}
        CheckifStuck();
        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude>15 && stuck){
            stuck = false;
        }
    }

    void CheckifStuck()
    {
        if (!checkingStuck)
        {
            StartCoroutine(CheckifStuckHelper());
        }
    }

    public void ResetCar()
    {
        stuck = false;
        StartCoroutine(ResetEnum()); //ResetEnum
    }
    public void ChangeRespawn()
    {
        //respawnPos = respawn;
        //if(i==0){
        respawnStruct.Set(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
        //}
        //else if (i==1){
        //    resetStruct.Set(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
        //}
    }

    public void Respawn(int i)
    {
        StartCoroutine(RespawnEnum(i));
    }

    void RespawnHelper()
    {
        //cameraMain.gameObject.transform.position = respawnStruct.respawnCameraMain;
        //cameraParent.GetComponent<CameraController>().enabled = false;
        cameraParent.gameObject.transform.position = respawnStruct.respawnCameraMain;
        //cameraMain.gameObject.transform.rotation = respawnStruct.cameraRotateMain;
        cameraParent.gameObject.transform.rotation = respawnStruct.cameraRotateParent;
        cameraParent.GetComponent<CameraController>().enabled = false;
        transform.position = respawnStruct.respawnPos;
        transform.rotation = respawnStruct.respawnRotate;
    }

    IEnumerator RespawnEnum(int i)
    {
        //if i ==1 this is a reset
        gameObject.GetComponent<ControllerInput>().enabled = false;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //car.SetActive(false);
        RespawnHelper();
        cameraParent.GetComponent<CameraController>().enabled = true;
        this.transform.Find("SkyCar").gameObject.SetActive(true);
        this.transform.Find("Arrow").gameObject.SetActive(true);
        if (i == 0) { gameObject.GetComponent<PlayerPickup>().Respawn(); }
        yield return new WaitForSeconds(5);
        //car.SetActive(true);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<ControllerInput>().enabled = true;
        checkingStuck = false;
        stuck = false;
    }

    IEnumerator CheckifStuckHelper()
    {
        //Check this!!
        checkingStuck = true;
        Vector3 pos1 = transform.position;
        float vel = carRb.velocity.magnitude;
        yield return new WaitForSeconds((int)waitTime / 2);
        float vel2 = carRb.velocity.magnitude;
        Vector3 pos2 = transform.position;
        yield return new WaitForSeconds((int)waitTime / 2);
        if ((int)vel == (int)carRb.velocity.magnitude && (int)vel == 0 && (int)vel == (int)vel2)
        {
            if(Check(pos1,pos2,transform.position))
            {
                stuck = true; 
            }            
        }
        else
        {
            if (carRb.velocity.magnitude > 15 && carRb.velocity.x>0 && carRb.velocity.z>0)
            {
                resetStruct2 = resetStruct;
                resetStruct.Set(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
            }
        }
        checkingStuck = false;
    }
    bool Check(Vector3 pos1,Vector3 pos2,Vector3 pos3){
        bool xStationary = false; bool zStationary = false;
        if((int)pos1.x==(int)pos2.x && (int)pos1.x == (int)pos3.x && (int)pos3.x == (int)pos2.x ){
            xStationary = true;
        }
        if ((int)pos1.z == (int)pos2.z && (int)pos1.z== (int)pos3.z && (int)pos3.z == (int)pos2.z)
        {
            zStationary = true;
        }
        if (xStationary && zStationary) { return true; }
        return false;
    }

    IEnumerator ResetEnum()
    {
        //have car flash
        for (int i = 0; i < 4; i++)
        {
            this.transform.Find("SkyCar").gameObject.SetActive(true);
            yield return new WaitForSeconds(.15f);
            this.transform.Find("SkyCar").gameObject.SetActive(false);
            yield return new WaitForSeconds(.15f);
        }
        yield return new WaitForSeconds(2);
        resetStruct.Set(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
        RespawnStruct temp = respawnStruct;
        respawnStruct = resetStruct2;
        Respawn(1);
        respawnStruct = temp;
        resetStruct = temp;
        resetStruct2 = temp;
    }

}

