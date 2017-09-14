// This script draws the labels, connection graphs and paths on respective view modes.
// This is an editor script thus has to be saved inside 'Editor' folder.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(AIWaypointNetwork))] // Custom editor of type AIWaypointNetwork (class).
public class AIWaypointNetworkEditor : Editor {

    // This function is required for making a custom inspector.
	public override void OnInspectorGUI() // This function has to be overriden in order to work.
	{
        AIWaypointNetwork network = (AIWaypointNetwork)target; // The object being inspected.
		network.displayMode = (PathDisplayMode)(EditorGUILayout.EnumPopup ("Display Mode", network.displayMode)); // Drawing dropdown menu for display modes.
		
        // Sliders for setting start and end waypoint will show up only if display mode is set to 'Paths'.
        if (network.displayMode == PathDisplayMode.Paths) {
			network.UIStart = EditorGUILayout.IntSlider ("Waypoint Start", network.UIStart, 0, network.Waypoints.Count - 1); // Draw start waypoint slider.
			network.UIEnd = EditorGUILayout.IntSlider ("Waypoint End", network.UIEnd, 0, network.Waypoints.Count - 1); // Draw end waypoint slider.
		}
		DrawDefaultInspector (); // After drawing custom widgets and stuffs the default inspector window will be drawn.
	}

    // Function used for handling events in the scene view.
    private void OnSceneGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target; // The object being inspected.

		// This loop prints out the labels of each waypoint on the scene view.
		for(int i = 0; i<network.Waypoints.Count; i++)
        {
			if (network.Waypoints [i] != null)
				
            	Handles.Label(network.Waypoints[i].position, "Waypoint_" + i.ToString());
        }

        // Drawing the connection graph among all waypoints.
		if (network.displayMode == PathDisplayMode.Connections) {
			// A Vector3 array with the element number of 'number-of-waypoints + 1' is being initialized.
			Vector3[] linePoints = new Vector3[network.Waypoints.Count + 1];

			// If the loop reaches the last waypoint in the array it'll point back to the first element in order to make a network among all the waypoints.
			for (int i = 0; i <= network.Waypoints.Count; i++) {
				int index = i;
				if (i != network.Waypoints.Count)
					index = i;
				else
					index = 0;
			
				// If current index is not null then plot a line point in current index position.
				if (network.Waypoints [index] != null)
					linePoints [i] = network.Waypoints [index].position;
				else
					linePoints [i] = new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
			}

			// Draw the polygonal line connecting all the points in 'linePoints' array.
			Handles.color = Color.cyan;
			Handles.DrawPolyLine (linePoints);
		} 
        // Draw lines connecting two selected waypoints using A* pathfinding.
        else if (network.displayMode == PathDisplayMode.Paths) {
			NavMeshPath path = new NavMeshPath (); // Object for saving calculated path.

			if (network.Waypoints [network.UIStart] != null && network.Waypoints [network.UIEnd] != null) {
				Vector3 from = network.Waypoints [network.UIStart].position;
				Vector3 to = network.Waypoints [network.UIEnd].position;

				NavMesh.CalculatePath (from, to, NavMesh.AllAreas, path);
				Handles.color = Color.yellow;
				Handles.DrawPolyLine (path.corners);
			}
		}
    }
}
