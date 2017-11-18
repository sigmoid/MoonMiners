using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<GameManager1>().StartLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
