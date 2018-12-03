using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {

    public Transform ball;
	
	// Update is called once per frame
	void Update () {
        transform.position = ball.position;
	}
}
