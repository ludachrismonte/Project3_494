using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityEngine.SceneManagement;

public class PostGameManager : MonoBehaviour 
{
    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;
    Transform p1tf;
    Transform p2tf;
    Transform p3tf;
    Transform p4tf;

    // Winner position
    // Pos (2, 2.63, -3.06)
    private Vector3 winVec = new Vector3(270.63f, 53.68f, 382.48f);
    // Rot (0, 160, 0)
    private Quaternion winRot = Quaternion.Euler(0, 92.17f, 0);

    // Player positions and rotations
    private Vector3 player1Pos = new Vector3(277.58f, 55.07f, 373.07f);
    private Quaternion player1Rot = Quaternion.Euler(180, 155.31f, 0);
    private Vector3 player2Pos = new Vector3(275.12f, 55.1f, 369.86f);
    private Quaternion player2Rot = Quaternion.Euler(180, -90, 0);
    private Vector3 player3Pos = new Vector3(279.55f, 55.07f, 368.79f);
    private Quaternion player3Rot = Quaternion.Euler(180, 51.05f, 0);
    private Vector3 player4Pos = new Vector3(272.87f, 55.06f, 374.6f);
    private Quaternion player4Rot = Quaternion.Euler(180, -235.4f, 0);

    public Text winText;
    public Text resetText;

    public GameObject fire;

    //Fire position and rotation
    private Vector3 firePos = new Vector3(0f, 0.75f, -0.25f);
    private Quaternion fireRot = Quaternion.Euler(0, 0, -180);
    private Vector3 fireScale = new Vector3(0.5f, 1f, 0.5f);

    private int winNum = 0;
    private bool canReset = false;

    // Use this for initialization
    void Start () 
    {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player3 = GameObject.FindGameObjectWithTag("Player3");
        player4 = GameObject.FindGameObjectWithTag("Player4");
        p1tf = player1.GetComponent<Transform>();
        p2tf = player2.GetComponent<Transform>();
        p3tf = player3.GetComponent<Transform>();
        p4tf = player4.GetComponent<Transform>();

        DisableComponents();

        // Deactivate controls
        player1.GetComponent<ControllerInput>().enabled = false;
        player1.GetComponent<Rigidbody>().isKinematic = true;
        player2.GetComponent<ControllerInput>().enabled = false;
        player2.GetComponent<Rigidbody>().isKinematic = true;
        player3.GetComponent<ControllerInput>().enabled = false;
        player3.GetComponent<Rigidbody>().isKinematic = true;
        player4.GetComponent<ControllerInput>().enabled = false;
        player4.GetComponent<Rigidbody>().isKinematic = true;

        // Initialize player positions
        p1tf.SetPositionAndRotation(player1Pos, player1Rot);
        p2tf.SetPositionAndRotation(player2Pos, player2Rot);
        p3tf.SetPositionAndRotation(player3Pos, player3Rot);
        p4tf.SetPositionAndRotation(player4Pos, player4Rot);

        // Set cars on fire
        GameObject flames = Instantiate(fire);
        Transform tf = flames.GetComponent<Transform>();
        tf.parent = p1tf;
        tf.localPosition = firePos;
        tf.localRotation = fireRot;
        tf.localScale = fireScale;

        flames = Instantiate(fire);
        tf = flames.GetComponent<Transform>();
        tf.parent = p2tf;
        tf.localPosition = firePos;
        tf.localRotation = fireRot;
        tf.localScale = fireScale;

        flames = Instantiate(fire);
        tf = flames.GetComponent<Transform>();
        tf.parent = p3tf;
        tf.localPosition = firePos;
        tf.localRotation = fireRot;
        tf.localScale = fireScale;

        flames = Instantiate(fire);
        tf = flames.GetComponent<Transform>();
        tf.parent = p4tf;
        tf.localPosition = firePos;
        tf.localRotation = fireRot;
        tf.localScale = fireScale;

        GameObject winner = FindWinner();
        winNum = winner.GetComponent<ControllerInput>().playerNum;
        winner.transform.SetPositionAndRotation(winVec, winRot);
        winText.text = "player " + (winNum + 1) + " wins!";
        switch(winNum){
            case 0:
                winText.color = Color.blue;
                break;
            case 1:
                winText.color = Color.red;
                break;
            case 2:
                winText.color = Color.green;
                break;
            case 3:
                winText.color = Color.yellow;
                break;
        }


        // Deactivate fire
        foreach (Transform child in winner.transform)
        {
            if (child.gameObject.tag == "Fire")
            {
                child.gameObject.SetActive(false);
            }
        }

        StartCoroutine(EndScene());
    }

    // Update is called once per frame
    void Update()
    {
        // Have the winning player control reset
        var p1 = (InputManager.Devices.Count > 0) ? InputManager.Devices[0] : null;
        var p2 = (InputManager.Devices.Count > 1) ? InputManager.Devices[1] : null;
        var p3 = (InputManager.Devices.Count > 2) ? InputManager.Devices[2] : null;
        var p4 = (InputManager.Devices.Count > 3) ? InputManager.Devices[3] : null;

        float resetInput = 0;
        if (p1 != null)
            resetInput = p1.Action1.Value; // A button
        if (p2 != null)
            resetInput += p2.Action1.Value;
        if (p3 != null)
            resetInput += p3.Action1.Value;
        if (p4 != null)
            resetInput += p4.Action1.Value;
        Debug.Log(resetInput);

        if ((canReset) && (resetInput > 0.0f))
        {
            Destroy(player1);
            Destroy(player2);
            Destroy(player3);
            Destroy(player4);
            //Destroy(DestroyOnLoad.instance.gameObject);
            SceneManager.LoadScene("Menu");
        }
    }

    private void DisableComponents()
    {
        player1.GetComponent<PlayerPickup>().enabled = false;
        player2.GetComponent<PlayerPickup>().enabled = false;
        player3.GetComponent<PlayerPickup>().enabled = false; 
        player4.GetComponent<PlayerPickup>().enabled = false;

        player1.GetComponent<CameraShake>().enabled = false;
        player2.GetComponent<CameraShake>().enabled = false;
        player3.GetComponent<CameraShake>().enabled = false;
        player4.GetComponent<CameraShake>().enabled = false;

        player1.GetComponent<RocketTargeter>().enabled = false;
        player2.GetComponent<RocketTargeter>().enabled = false;
        player3.GetComponent<RocketTargeter>().enabled = false;
        player4.GetComponent<RocketTargeter>().enabled = false;

        player1.GetComponent<Compass>().enabled = false;
        player2.GetComponent<Compass>().enabled = false;
        player3.GetComponent<Compass>().enabled = false;
        player4.GetComponent<Compass>().enabled = false;
    }

    IEnumerator EndScene ()
    {
        yield return new WaitForSeconds(3.0f);
        resetText.enabled = true;
        canReset = true;

        while(true){
            yield return new WaitForSeconds(0.5f);
            resetText.enabled = false;
            yield return new WaitForSeconds(0.5f);
            resetText.enabled = true;

        }
    }

    public GameObject FindWinner()
    {
        int max = player1.GetComponent<Score>().GetCurrentScore();
        GameObject winner = player1;

        // Check player 2
        int tempScore = player2.GetComponent<Score>().GetCurrentScore();

        if (tempScore > max){
            max = tempScore;
            winner = player2;
        }

        // Check player 3
        tempScore = player3.GetComponent<Score>().GetCurrentScore();

        if (tempScore > max)
        {
            max = tempScore;
            winner = player3;
        }

        // Check player 4
        tempScore = player4.GetComponent<Score>().GetCurrentScore();

        if (tempScore > max)
        {
            max = tempScore;
            winner = player4;
        }

        return winner;

    }
}
