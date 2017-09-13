using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentExample : MonoBehaviour {
	
	public AIWaypointNetwork wayPointNetwork = null;
	public int currentIndex = 0;

	public bool hasPath = false;
	public bool pathPending = false;
	public bool isPathStale = false;

	private NavMeshAgent _navAgent = null;

	// Use this for initialization
	void Start () {
		_navAgent = GetComponent<NavMeshAgent> ();

		if (wayPointNetwork == null)
			return;
//		if (wayPointNetwork.Waypoints [currentIndex] != null) {
//			_navAgent.destination = wayPointNetwork.Waypoints [currentIndex].position;
//		}
		SetNextDestination(false);
	}

	void SetNextDestination(bool increment){
		if (!wayPointNetwork)
			return;

		int incrementStep;
		if (increment)
			incrementStep = 1;
		else
			incrementStep = 0;

		Transform nextWaypointTransform = null;

		while (nextWaypointTransform == null) {
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
		}
		currentIndex++;
	}

	// Update is called once per frame
	void Update () {
		hasPath = _navAgent.hasPath;
		pathPending = _navAgent.pathPending;
		isPathStale = _navAgent.isPathStale;

		if (!hasPath && !pathPending)
			SetNextDestination (true);
		else if (isPathStale)
			SetNextDestination (false);
	}
}
