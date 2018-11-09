using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    //public int playerId;
    float maxHealth;
    float currHealth;
    GameObject healthBar;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        healthBar = this.transform.Find("InnerBar").gameObject;
        //define max health and current health
    }

    // Update is called once per frame
    void Update()
    {
        //update current health
        UpdateBarHealth();
    }
    void UpdateBarHealth()
    {
        //Debug.Log(currHealth / maxHealth);
        currHealth = player.GetComponent<Health>().health;
        maxHealth = player.GetComponent<Health>().maxHealth;
        healthBar.transform.localScale = new Vector3(currHealth / maxHealth, 1, 1);
    }
}
