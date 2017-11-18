using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCollisionInvoker : MonoBehaviour {

    PlayerController _ParentController;

	// Use this for initialization
	void Start () {
        _ParentController = transform.parent.parent.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        _ParentController.OnCaptureCollision(other.gameObject);
    }
}
