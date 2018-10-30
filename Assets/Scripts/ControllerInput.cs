using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerInput : MonoBehaviour {

    public int playerNum;
    private UnityStandardAssets.Vehicles.Car.CarController car;
    //InputDevice player;

	// Use this for initialization
	void Start () {

        car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();

        // Assign input device for a controller based on player num
        //player = InputManager.Devices[playerNum];
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        var player = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        if (player == null)
        {
            return;
        }
        else
        {
            // Horizontal (left/right)
            float h = player.LeftStick.Vector.x;

            // Acceleration
            float v = player.RightTrigger.Value;

            // If the right trigger isn't being used, go in reverse
            if (v <= 0.0f)
            {
                v = -player.LeftTrigger.Value;
            }

            float handbrake = player.Action3.Value;

            // Move car based on inputs
            car.Move(h, v, v, handbrake);

            //Debug.Log(h + "," + v + "," + handbrake);
        }


    }
}
