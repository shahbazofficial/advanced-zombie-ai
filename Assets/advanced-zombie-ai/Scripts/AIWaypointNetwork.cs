// This class stores all the necessary variables for visualising the network path.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enumerator containing all the path display modes, presented via a dropdown menu.
public enum PathDisplayMode {
	None,
	Connections,
	Paths
}

public class AIWaypointNetwork : MonoBehaviour {

	[HideInInspector] public PathDisplayMode displayMode = PathDisplayMode.None; // Dropdown menu populated with the elemnts from PathDisplayMode enumerator.
	[HideInInspector] public int UIStart = 0; // Starting waypoint for visualising the path.
	[HideInInspector] public int UIEnd = 0; // Ending waypoint for visualising the path.
    public List<Transform> Waypoints = new List<Transform>(); // List of all waypoints laid in the level.

}
