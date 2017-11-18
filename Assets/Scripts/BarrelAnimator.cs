using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelAnimator : MonoBehaviour {

    [SerializeField]
    private float CloseDuration = 0.125f;

    //Actual gameobjects
    public Transform LeftBarrel, RightBarrel;

    public Transform LeftStart, RightStart, LeftEnd, RightEnd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenBarrel()
    {
        LeftBarrel.rotation = LeftEnd.rotation;
        RightBarrel.rotation = RightEnd.rotation;
        LeftBarrel.position = LeftEnd.position;
        RightBarrel.position = RightEnd.position;
    }

    public void CloseBarrel()
    {
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        float lerpval = 0;
        float timer = CloseDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            lerpval = timer / CloseDuration;

            LeftBarrel.transform.rotation = Quaternion.Lerp(LeftStart.rotation, LeftEnd.rotation, lerpval);
            RightBarrel.transform.rotation = Quaternion.Lerp(RightStart.rotation, RightEnd.rotation, lerpval);
            LeftBarrel.transform.position = Vector3.Slerp(LeftStart.position, LeftEnd.position, lerpval);
            RightBarrel.transform.position = Vector3.Slerp(RightStart.position, RightEnd.position, lerpval);
            yield return null;
        }

        yield break;
    }
}
