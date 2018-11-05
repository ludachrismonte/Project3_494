using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
//using UnityEngine.UI;

public class ReadyUpSprite : MonoBehaviour {

    // For finding proper controller input from InControl
    public int playerNum;

    // Starting/ending size of the car sprite
    public float startscale = 1;
    public float maxScale = 8.0f;

    // How fast players can rotate their sprites
    public float rotateSpeed = 2.0f;

    // Image to display once player is readied up
    public GameObject readyImg;

    Transform tf;

	// Use this for initialization
	void Start () {
        tf = GetComponent<Transform>();
	}

    // Update is called once per frame
    void Update()
    {

        var player = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        if (player == null)
        {
            return;
        }
        else
        {
            // Get R trigger input
            float v = player.RightTrigger.Value;

            // Getting bigger
            if (v > 0.0f){
                tf.localScale += new Vector3(0.05f, 0.05f, 0.0f);
            }
            // Getting smaller
            else if ((v <= 0.0f) && (tf.localScale.x > startscale)){
                tf.localScale += new Vector3(-0.05f, -0.05f, 0.0f);
            }

            // Horizontal (left/right)
            float h = player.LeftStick.Vector.x;
            //Quaternion old = tf.rotation;
            //tf.rotation += Quaternion.Euler(0.0f, 0.0f, h);

            tf.Rotate(new Vector3(0.0f, 0.0f, -h * rotateSpeed));

            // Check for readied up
            if (tf.localScale.x >= maxScale){
                Debug.Log("PLayer " + playerNum + " readied up");
                ReadyUpManager.numReadied++;
                readyImg.SetActive(true);
                this.enabled = false;
            }

        }


    }
}
