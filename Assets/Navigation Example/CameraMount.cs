using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMount : MonoBehaviour {

    public Transform cameraMount = null;
    public float speed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position, cameraMount.transform.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraMount.rotation, Time.deltaTime * speed);
	}
}
