using UnityEngine;
using System.Collections;

public class ExpansionScript : BuildingScript
{
	public GameObject m_expansionPointPrefab;
	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
	{
		if (m_health <= 0)
		{
			GameObject expansionPoint = Instantiate(m_expansionPointPrefab) as GameObject;
			expansionPoint.transform.position = transform.position;
			expansionPoint.transform.parent = GameObject.FindGameObjectWithTag("Map").transform;
			transform.parent.GetComponent<PlayerScript>().m_resourcePoints--;
			Destroy(gameObject);
		}
	}
}
