// Script for controlling a placeholder NavMeshAgent on the scene.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgent : MonoBehaviour {
//public class NavAgentExample : MonoBehaviour {
	
    // Public members
	public AIWaypointNetwork wayPointNetwork = null;
	public int currentIndex = 0;
	public bool hasPath = false;
	public bool pathPending = false;
	public bool isPathStale = false;
    public NavMeshPathStatus pathStatus = NavMeshPathStatus.PathInvalid;
    public AnimationCurve jumpCurve = new AnimationCurve();

    // Private members
	private NavMeshAgent _navAgent = null;

	// Use this for initialization
	void Start () {
		_navAgent = GetComponent<NavMeshAgent> ();

//        _navAgent.updatePosition = false;
//        _navAgent.updateRotation = false;

		if (wayPointNetwork == null)
			return;
		SetNextDestination(false);
	}

    // This function sets the next waypoint as destination depending on certain conditions.
	void SetNextDestination(bool increment){
		if (!wayPointNetwork)
			return;

		int incrementStep;
		if (increment)
			incrementStep = 1;
		else
			incrementStep = 0;

		Transform nextWaypointTransform = null;
        
		int nextWaypoint;
		if ((currentIndex + incrementStep) >= wayPointNetwork.Waypoints.Count)
			nextWaypoint = 0;
		else
			nextWaypoint = currentIndex + incrementStep;

		nextWaypointTransform = wayPointNetwork.Waypoints [nextWaypoint];
		if (nextWaypointTransform != null) {
			currentIndex = nextWaypoint;
			_navAgent.destination = nextWaypointTransform.position;
			return;
		}
		currentIndex++;
	}

	// Update is called once per frame
	void Update () {
		hasPath = _navAgent.hasPath;
		pathPending = _navAgent.pathPending;
		isPathStale = _navAgent.isPathStale;
        pathStatus = _navAgent.pathStatus;

        if(_navAgent.isOnOffMeshLink)
        {
            StartCoroutine(Jump(1.0f));
            return;
        }

        if ((_navAgent.remainingDistance <= _navAgent.stoppingDistance && !pathPending) || pathStatus == NavMeshPathStatus.PathInvalid /*|| pathStatus == NavMeshPathStatus.PathPartial*/)
        {
            SetNextDestination (true);
        }
        else if (isPathStale)
			SetNextDestination (false);
	}

    IEnumerator Jump(float duration)
    {
        OffMeshLinkData linkData = _navAgent.currentOffMeshLinkData;
        Vector3 startPos = _navAgent.transform.position;
        Vector3 endPos = linkData.endPos + (_navAgent.baseOffset * Vector3.up);
        float time = 0.0f;

        while(time <= duration)
        {
            float t = time / duration;
            _navAgent.transform.position = Vector3.Lerp(startPos, endPos, t) + (jumpCurve.Evaluate(t) * Vector3.up);
            time += Time.deltaTime;
            yield return null;
        }
        _navAgent.CompleteOffMeshLink();
    }
}
