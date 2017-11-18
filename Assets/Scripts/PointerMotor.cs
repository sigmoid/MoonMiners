using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMotor : MonoBehaviour {

    [SerializeField]
    private float _PointerRotationRate = 0.25f;

    private float _Angle = 0, _PrevAngle = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        _Angle = Mathf.Sin(Time.time / _PointerRotationRate) * 60;
        transform.RotateAround(transform.parent.position, Vector3.forward ,_Angle - _PrevAngle);
        _PrevAngle = _Angle;
	}
}
