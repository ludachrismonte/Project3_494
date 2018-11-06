using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerInput : MonoBehaviour {

    public int playerNum;
    private UnityStandardAssets.Vehicles.Car.CarController car;
    private WeaponManager weapon_manager; 

	void Start ()
    {
        car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        weapon_manager = GetComponent<WeaponManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
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

            if (v <= 0.0f)
            {
                v = -player.LeftTrigger.Value;
            }

            float handbrake = player.Action3.Value;

            // Move car based on inputs
            car.Move(h, v, v, handbrake);

            // Weapons

            float fire = player.Action2.Value;
            if (fire != 0.0f) { weapon_manager.fire(); }
        }
    }
}
