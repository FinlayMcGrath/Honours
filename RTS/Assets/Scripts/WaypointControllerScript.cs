using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointControllerScript : MonoBehaviour
{
	public List<GameObject> m_waypoints;

	// Use this fList<>or initialization
	void Start ()
	{
		m_waypoints = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public void AddWaypoints()
	{
		m_waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
	}
}
