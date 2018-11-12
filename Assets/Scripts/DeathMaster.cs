using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMaster : MonoBehaviour {

    public IEnumerator Die(GameObject player)
    {
        player.GetComponent<ControllerInput>().enabled = false;
        GameObject car = player.transform.Find("SkyCar").gameObject;
        car.SetActive(false);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 4; i++) {
            car.SetActive(true);
            yield return new WaitForSeconds(.15f);
            car.SetActive(false);
            yield return new WaitForSeconds(.15f);
        }
        car.SetActive(true);
        player.GetComponent<ControllerInput>().enabled = true;
    }
}
