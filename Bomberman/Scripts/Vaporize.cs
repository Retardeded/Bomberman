using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaporize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.15f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
