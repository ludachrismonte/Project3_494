using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AllPlayersHit : MonoBehaviour {

    public bool one = false;
    public bool two = false;
    public bool three = false;
    public bool four = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { one = true; }
        if (other.tag == "Player2") { two = true; }
        if (other.tag == "Player3") { three = true; }
        if (other.tag == "Player4") { four = true; }
        Debug.Log("hit one");
    }

    // Update is called once per frame
    void Update () {
        if (one && two && three && four) {
            StartCoroutine(changeScene());
        }
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Arena");
    }
}
