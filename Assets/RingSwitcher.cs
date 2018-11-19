using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSwitcher : MonoBehaviour {

    public GameObject[] Rings;

    private int index_active;

    private void Start()
    {
        for (int i = 0; i < Rings.Length; i++) {
            Rings[i].SetActive(false);
        }
        index_active = (int)Random.Range(0, Rings.Length);
        Rings[index_active].SetActive(true);
    }

    // Update is called once per frame
    public void Switch () {
        Rings[index_active].SetActive(false);
        int temp = (int)Random.Range(0, Rings.Length);
        while (temp == index_active) {
            temp = (int)Random.Range(0, Rings.Length);
        }
        index_active = temp;
        Rings[index_active].SetActive(true);
    }
}
