using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassTutorial : MonoBehaviour
{

    public GameObject UI_Folder;
    public int my_num;
    public Camera cam;
    public Transform m_PlayerFlag;
    
    private Transform player1;
    private Transform player2;
    private Transform player3;
    private Transform player4;

    private Transform left_loc;
    private Transform right_loc;
    private Image i1;
    private Image i2;
    private Image i3;
    private Image i4;
    private Image flag;

    private void Start()
    {
        left_loc = UI_Folder.transform.Find("Left").transform;
        right_loc = UI_Folder.transform.Find("Right").transform;

        player1 = GameObject.FindWithTag("Player").transform;
        player2 = GameObject.FindWithTag("Player2").transform;
        player3 = GameObject.FindWithTag("Player3").transform;
        player4 = GameObject.FindWithTag("Player4").transform;

        i1 = UI_Folder.transform.Find("i1").GetComponent<Image>();
        i2 = UI_Folder.transform.Find("i2").GetComponent<Image>();
        i3 = UI_Folder.transform.Find("i3").GetComponent<Image>();
        i4 = UI_Folder.transform.Find("i4").GetComponent<Image>();

        flag = UI_Folder.transform.Find("flag").GetComponent<Image>();

        if (my_num == 1)
            i1.gameObject.SetActive(false);
        if (my_num == 2)
            i2.gameObject.SetActive(false);
        if (my_num == 3)
            i3.gameObject.SetActive(false);
        if (my_num == 4)
            i4.gameObject.SetActive(false);
    }

    private void Track(Image img, Transform player)
    {
        Vector3 pos = cam.WorldToScreenPoint(player.transform.position);

        Vector2 targetDir = new Vector2(player.position.x - cam.transform.position.x, player.position.z - cam.transform.position.z);
        Vector2 newForward = new Vector2(cam.transform.forward.x, cam.transform.forward.z);
        float angle = Vector3.Angle(targetDir, newForward);

        img.transform.position = angle < 27 ?
            new Vector3(pos.x, img.transform.position.y, 0) :
            ObjectToRight(newForward, targetDir) ?
                right_loc.position :
                left_loc.position;
    }

    private bool ObjectToRight(Vector3 fwd, Vector3 targetDir)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        return perp.z < 0.0f;
    }

    void LateUpdate()
    {
        i1.enabled = true;
        i2.enabled = true;
        i3.enabled = true;
        i4.enabled = true;

        if (m_PlayerFlag != null)
        {
            flag.enabled = true;
            flag.color = Color.white;
            Track(flag, m_PlayerFlag);
        }
        else
        {
            flag.enabled = false;
        }

        if (my_num != 1)
            Track(i1, player1);
        if (my_num != 2)
            Track(i2, player2);
        if (my_num != 3)
            Track(i3, player3);
        if (my_num != 4)
            Track(i4, player4);
    }
}