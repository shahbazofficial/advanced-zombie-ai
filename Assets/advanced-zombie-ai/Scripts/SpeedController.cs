using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour {

    Animator myAnimator = null;
    public float speed = 0.0f;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        myAnimator.SetFloat("Speed", speed);
	}
}
