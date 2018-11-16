using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //public Vector3 repawnPos;
    //public object respawnPos { get; private set; }
    GameObject cameraParent;
    GameObject cameraMain;
    // Use this for initialization

    private struct RespawnStruct {
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
        public void Set(Vector3 carPos, Vector3 mainCamPos, Vector3 camParentPos, Quaternion carRotate, Quaternion mainCamRotate, Quaternion camParentRotate){
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

    RespawnStruct respawnStr;
    void Awake()
    {
        cameraParent = this.transform.Find("CameraParent").gameObject;
        cameraMain = this.transform.Find("MainCamera").gameObject;
        respawnStr = new RespawnStruct(transform.position,cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation,cameraParent.gameObject.transform.rotation);

    }

    // Update is called once per frame
    void Update() {

    }
    public void changeRespawn() {
        //respawnPos = respawn;
        respawnStr.Set(transform.position, cameraMain.gameObject.transform.position, cameraParent.gameObject.transform.position, transform.rotation, cameraMain.gameObject.transform.rotation, cameraParent.gameObject.transform.rotation);
    }

    public void respawn() {
        StartCoroutine(RespawnEnum());
    }

    void respawnHelper(){
        transform.position = respawnStr.respawnPos;
        transform.rotation = respawnStr.respawnRotate;
        cameraMain.gameObject.transform.position = respawnStr.respawnCameraMain;
        cameraParent.gameObject.transform.position = respawnStr.respawnCameraMain;
        cameraMain.gameObject.transform.rotation = respawnStr.cameraRotateMain;
        cameraParent.gameObject.transform.rotation = respawnStr.cameraRotateParent;
    }

    IEnumerator RespawnEnum(){
        gameObject.GetComponent<ControllerInput>().enabled = false;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        respawnHelper();
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<ControllerInput>().enabled = true;
    }

}
