using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {

    public int boostType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().GetBoost(boostType);
            gameObject.SetActive(false);
        }
    }
    public void SetBoostType(int num)
    {
        boostType = num;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
