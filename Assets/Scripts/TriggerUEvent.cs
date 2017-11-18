using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerUEvent : MonoBehaviour {

    [SerializeField]
    private UnityEvent _Event;

    public string Mask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BallScript>() != null)
         _Event.Invoke();
    }
}
