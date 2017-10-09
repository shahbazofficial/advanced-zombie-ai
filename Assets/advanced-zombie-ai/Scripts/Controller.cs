using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    Animator myAnimator = null;
    private int horizontalHash = 0;
    private int verticalHash = 0;
    private int attackHash = 0;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
        horizontalHash = Animator.StringToHash("Horizontal");
        verticalHash = Animator.StringToHash("Vertical");
        attackHash = Animator.StringToHash("Attack");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            myAnimator.SetTrigger(attackHash);
        float xAxis = Input.GetAxis("Horizontal") * 2.32f;
        float zAxis = Input.GetAxis("Vertical") * 5.66f;

        myAnimator.SetFloat(horizontalHash, xAxis, 0.1f, Time.deltaTime);
        myAnimator.SetFloat(verticalHash, zAxis, 1.0f, Time.deltaTime);
	}
}
