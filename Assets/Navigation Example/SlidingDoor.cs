using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorStates
{
    Open,
    Animating,
    Closed
}

public class SlidingDoor : MonoBehaviour {

    // Public members
    public float slidingDistance = 4.0f;
    public float duration = 1.5f;
    public AnimationCurve moveCurve = new AnimationCurve();

    // Private members
    private Transform _doorTransform = null;
    private Vector3 _openPos = Vector3.zero;
    private Vector3 _closedPos = Vector3.zero;
    private DoorStates _currentDoorState = DoorStates.Closed;

	// Use this for initialization
	void Start () {
        _doorTransform = GetComponent<Transform>();
        _closedPos = _doorTransform.position;
        _openPos = _closedPos + (slidingDistance * _doorTransform.right);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && _currentDoorState != DoorStates.Animating)
        {
            if (_currentDoorState == DoorStates.Closed)
                StartCoroutine(AnimateDoor(DoorStates.Open));
            else
                StartCoroutine(AnimateDoor(DoorStates.Closed));
        }
	}

    IEnumerator AnimateDoor (DoorStates newState)
    {
        _currentDoorState = DoorStates.Animating;
        float time = 0.0f;
        Vector3 startPos;
        Vector3 endPos;

        if (newState == DoorStates.Open)
            startPos = _closedPos;
        else
            startPos = _openPos;

        if (newState == DoorStates.Open)
            endPos = _openPos;
        else
            endPos = _closedPos;

        while(time <= duration)
        {
            float t = time / duration;
            _doorTransform.position = Vector3.Lerp(startPos, endPos, moveCurve.Evaluate(t));
            time += Time.deltaTime;
            yield return null;
        }
        _doorTransform.position = endPos;
        _currentDoorState = newState;
    }
}
