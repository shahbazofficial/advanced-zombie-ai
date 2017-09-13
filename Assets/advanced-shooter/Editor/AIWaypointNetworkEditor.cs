using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(AIWaypointNetwork))]
public class AIWaypointNetworkEditor : Editor {

	public override void OnInspectorGUI()
	{
		AIWaypointNetwork network = (AIWaypointNetwork)target;
		network.displayMode = (PathDisplayMode)(EditorGUILayout.EnumPopup ("Display Mode", network.displayMode));
		if (network.displayMode == PathDisplayMode.Paths) {
			network.UIStart = EditorGUILayout.IntSlider ("Waypoint Start", network.UIStart, 0, network.Waypoints.Count - 1);
			network.UIEnd = EditorGUILayout.IntSlider ("Waypoint End", network.UIEnd, 0, network.Waypoints.Count - 1);
		}
		DrawDefaultInspector ();
	}

    private void OnSceneGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;

		// This loop prints out the labels of each waypoint on the scene view.
        for(int i = 0; i<network.Waypoints.Count; i++)
        {
			if (network.Waypoints [i] != null)
				
            	Handles.Label(network.Waypoints[i].position, "Waypoint_" + i.ToString());
        }

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
		} else if (network.displayMode == PathDisplayMode.Paths) {
			NavMeshPath path = new NavMeshPath ();

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
