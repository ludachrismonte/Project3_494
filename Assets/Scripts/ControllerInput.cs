using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerInput : MonoBehaviour
{
    public int playerNum;

    private readonly float torque = 15000.0f;

    private UnityStandardAssets.Vehicles.Car.CarController car;
    private WeaponManager weapon_manager;
    private Transform player_one;
    private Transform player_two;
    private Transform player_three;
    private Transform player_four;

    private Rigidbody m_Rigidbody;

    private int target_loc;
    private Vector3 offset;
    private Collider m_Collider;
    private float cooldown = .2f;
    private float distToGround = 2f;

    void Start()
    {
        car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        weapon_manager = GetComponent<WeaponManager>();
        player_one = GameObject.FindGameObjectWithTag("Player").transform;
        player_two = GameObject.FindGameObjectWithTag("Player2").transform;
        player_three = GameObject.FindGameObjectWithTag("Player3").transform;
        player_four = GameObject.FindGameObjectWithTag("Player4").transform;
        target_loc = -1;
        offset = new Vector3(0, 5, 0);

        m_Collider = GetComponent<Collider>();
        m_Rigidbody = GetComponent<Rigidbody>();
        distToGround = m_Collider.bounds.extents.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cooldown -= Time.deltaTime;
        var player = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        if (player == null)
        {
            return;
        }
        else
        {
            float horizInput = player.LeftStick.Vector.x;
            float vertInput = player.LeftStick.Vector.y;
            float rightHorizInput = player.RightStick.Vector.x;
            float gasInput = player.Action1.Value; // A button
            // Braking/reverse (X button)
            if (gasInput <= 0.0f)
            {
                gasInput = -player.Action3.Value;
            }

            bool reset = player.Action4.WasPressed;
            if (reset && this.GetComponent<RespawnReset>().stuck)
            {
                GetComponent<RespawnReset>().ResetCar();
                return;
            }

            if (!IsGrounded())
            {
                if (!Mathf.Approximately(0.0f, horizInput))
                {
                    m_Rigidbody.AddRelativeTorque(Vector3.up * horizInput * torque);
                }
                if (!Mathf.Approximately(0.0f, vertInput))
                {
                    m_Rigidbody.AddRelativeTorque(Vector3.right * vertInput * torque);
                }
            }
            else
            {
                car.Move(horizInput, gasInput, gasInput, 0);
            }

            // Weapons
            if (player.Action2.WasPressed) 
            { 
                weapon_manager.Fire(); 
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}