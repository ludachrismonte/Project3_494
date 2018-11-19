using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerInput : MonoBehaviour
{
    public int playerNum;
    public float m_Speed;

    private UnityStandardAssets.Vehicles.Car.CarController car;
    private WeaponManager weapon_manager;
    private Transform player_one;
    private Transform player_two;
    private Transform player_three;
    private Transform player_four;
    private Transform to_follow;

    private Rigidbody m_Rigidbody;

    private int target_loc;
    private Vector3 offset;
    private Collider m_Collider;
    public GameObject targeter = null;
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
        targeter.SetActive(false);
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
                    transform.Rotate(Vector3.up, horizInput * m_Speed * Time.deltaTime);
                }
                if (!Mathf.Approximately(0.0f, vertInput))
                {
                    transform.Rotate(Vector3.right, vertInput * m_Speed * Time.deltaTime);
                }
            }
            else
            {
                car.Move(horizInput, gasInput, gasInput, 0);
            }

            // Weapons
            bool fire = player.Action2.WasPressed;
            if (fire) { weapon_manager.fire(); }

            float targetLeft = player.LeftBumper.Value;     // Left bumper
            float targetRight = player.RightBumper.Value;   // Right bumper

            if ((!Mathf.Approximately(0.0f, targetLeft) || !Mathf.Approximately(0.0f, targetRight)) &&
                targeter != null && cooldown <= 0.0f)
            {
                if (!Mathf.Approximately(0.0f, targetLeft))
                    ToggleTarget(true);
                else
                    ToggleTarget(false);
            }

            if (to_follow != null)
            {
                targeter.transform.position = to_follow.transform.position + offset;
            }
        }
    }

    private void ToggleTarget(bool left)
    {
        cooldown = .2f;
        targeter.SetActive(true);
        if (left)
        {
            target_loc--;
            if (target_loc < 0)
            {
                target_loc = 3;
            }
        }
        else
        {
            target_loc++;
        }

        if (target_loc % 4 == playerNum)
        {
            if (left)
            {
                target_loc--;
                if (target_loc < 0)
                {
                    target_loc = 3;
                }
            }
            else
            {
                target_loc++;
            }
        }

        // Assign target based on player num
        if (target_loc % 4 == 0)
        {
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

    public GameObject GetTargeted()
    {
        return to_follow ? to_follow.gameObject : null;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}