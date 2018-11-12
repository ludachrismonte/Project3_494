using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerInput : MonoBehaviour {

    public int playerNum;
    private UnityStandardAssets.Vehicles.Car.CarController car;
    private WeaponManager weapon_manager;

    private Transform player_one;
    private Transform player_two;
    private Transform player_three;
    private Transform player_four;
    private Transform to_follow;
    private int target_loc;
    private Vector3 offset;
    public GameObject targeter = null;
    private float cooldown = .2f;
    
    void Start ()
    {
        car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        weapon_manager = GetComponent<WeaponManager>();
        player_one = GameObject.FindGameObjectWithTag("Player").transform;
        player_two = GameObject.FindGameObjectWithTag("Player2").transform;
        player_three = GameObject.FindGameObjectWithTag("Player3").transform;
        player_four = GameObject.FindGameObjectWithTag("Player4").transform;
        target_loc = -1;
        targeter.SetActive(false);
        offset = new Vector3(0, 5, 0);
    }

    // Update is called once per frame
    void LateUpdate () 
    {
        cooldown -= Time.deltaTime;
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
            float v = player.Action1.Value;

            if (v <= 0.0f)
            {
                v = -player.Action2.Value;
            }

            //float handbrake = player.Action3.Value;

            // Move car based on inputs
            car.Move(h, v, v, 0);

            // Weapons

            bool fire = player.Action3.WasPressed;
            if (fire) { weapon_manager.fire(); }

            float target = player.Action4.Value;
            if (target != 0 && targeter != null && cooldown <= 0.0f) {
                ToggleTarget();
            }

            if (to_follow != null) {

                targeter.transform.position = to_follow.transform.position + offset;
            }
        }
    }

    private void ToggleTarget() {
        cooldown = .2f;
        targeter.SetActive(true);
        target_loc++;
        if (target_loc % 4 == playerNum) {
            target_loc++;
        }
        if (target_loc % 4 == 0) {
            to_follow = player_one;
        }
        else if (target_loc % 4 == 1)
        {
            to_follow = player_two;
        }
        else if (target_loc % 4 == 2)
        {
            to_follow = player_three;
        }
        else if (target_loc % 4 == 3)
        {
            to_follow = player_four;
        }
    }

    public GameObject getTargeted()
    {
        if (to_follow) {
            return to_follow.gameObject;
        }
        return null;
    }
}
