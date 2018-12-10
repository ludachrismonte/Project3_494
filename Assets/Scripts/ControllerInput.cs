using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerInput : MonoBehaviour
{
    public int playerNum;
    public bool m_IsRUOrTutorial = false;

    private readonly float torque = 15000.0f;

    private UnityStandardAssets.Vehicles.Car.CarController car;
    private WeaponManager weapon_manager;

    private Rigidbody m_Rigidbody;

    private Collider m_Collider;
    private float cooldown = .2f;
    private float distToGround = 4f;

    void Start()
    {
        car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        weapon_manager = GetComponent<WeaponManager>();

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

        if (m_IsRUOrTutorial && player.MenuWasPressed)
        {
            PlayerTutorialManager playerTutorialManager = GetComponent<PlayerTutorialManager>();
            if (playerTutorialManager == null)
            {
                Debug.LogError("ERROR: PlayerTutorialMangaer does not exist");
                Application.Quit();
                return;
            }
            ReadyUpManager.instance.PlayersReadiedUp((PlayerNumber)playerNum, playerTutorialManager);
        }

        float horizInput = player.LeftStick.Vector.x;
        float vertInput = player.LeftStick.Vector.y;
        float gasInput = player.Action1.Value; // A button
        float brakeInput = -player.Action3.Value;

        // Braking/reverse (X button)
        //if (gasInput <= 0.0f)
        //{
        //    gasInput = -player.Action3.Value;
        //}
        
        if (player.Action4.WasPressed && GetComponent<RespawnReset>().stuck && transform.position.y >= 2.5)
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

        car.Move(horizInput, gasInput, brakeInput, 0);

        // Weapons
        if (player.Action2.WasPressed) 
        { 
            weapon_manager.Fire(); 
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}