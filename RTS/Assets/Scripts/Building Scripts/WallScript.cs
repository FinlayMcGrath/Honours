using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallScript : BuildingScript
{
	public GameObject m_wallPointPrefab;

	// Use this for initialization
	void Start()
	{
		m_health = 1000;
	}

	// Update is called once per frame
	void Update()
	{
		if (m_health <= 0)
		{
			GameObject wallPoint = Instantiate(m_wallPointPrefab) as GameObject;
			wallPoint.transform.position = transform.position;
			wallPoint.transform.parent = GameObject.FindGameObjectWithTag("Map").transform;
			Destroy(gameObject);
		}
	}
}
