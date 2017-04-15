using UnityEngine;
using System.Collections;

public class ArcheryRangeScript : BuildingScript {

	public GameObject m_archerPrefab, m_crossbowmanPrefab, m_catapultPrefab;

	// Use this for initialization
	void Start ()
	{
		m_cost = 50;
		m_buildingTime = 5;
		m_timeBuilding = 0;
		m_archerPrefab.layer = gameObject.layer;
		m_crossbowmanPrefab.layer = gameObject.layer;
		m_catapultPrefab.layer = gameObject.layer;
	}

	//if archery range isn't busy, start building an archer and return the cost of the archer
	public int BuildArcher(float money)
	{
		int cost = m_archerPrefab.GetComponent<ArcherScript>().GetCost();
		if (!m_busy && money > cost)
		{
			m_busy = true;
			m_unitTraining = m_archerPrefab;
			m_unitScript = m_unitTraining.GetComponent<ArcherScript>();
			return m_unitScript.GetCost();
		}
		return 0;
	}

	//if archery range isn't busy, start building a crossbowman and return the cost of the archer
	public int BuildCrossbowman(float money)
	{
		int cost = m_crossbowmanPrefab.GetComponent<CrossbowmanScript>().GetCost();
		if (!m_busy && money > cost)
		{
			m_busy = true;
			m_unitTraining = m_crossbowmanPrefab;
			m_unitScript = m_unitTraining.GetComponent<CrossbowmanScript>();
			return m_unitScript.GetCost();
		}
		return 0;
	}

	//if archery range isn't busy, start building a catapult and return the cost of the catapult
	public int BuildCatapult(float money)
	{
		int cost = m_catapultPrefab.GetComponent<CatapultScript>().GetCost();
		if (!m_busy && money > cost)
		{
			m_busy = true;
			m_unitTraining = m_catapultPrefab;
			m_unitScript = m_unitTraining.GetComponent<CatapultScript>();
			return m_unitScript.GetCost();
		}
		return 0;
	}
}
