using UnityEngine;
using System.Collections;

public class StablesScript : BuildingScript {

	public GameObject m_knightPrefab, m_horsemanPrefab;

	// Use this for initialization
	void Start()
	{
		m_cost = 50;
		m_buildingTime = 10;
		m_timeBuilding = 0;
		m_knightPrefab.layer = gameObject.layer;
		m_horsemanPrefab.layer = gameObject.layer;
	}

	//if archery range isn't busy, start building an archer and return the cost of the archer
	public int BuildKnight(float money)
	{
		int cost = m_knightPrefab.GetComponent<UnitScript>().GetCost();
		if (!m_busy && money > cost)
		{
			m_busy = true;
			m_unitTraining = m_knightPrefab;
			m_unitScript = m_unitTraining.GetComponent<UnitScript>();
			return m_unitScript.GetCost();
		}
		return 0;
	}

	//if archery range isn't busy, start building a horseman and return the cost of the horseman
	public int BuildHorseman(float money)
	{
		int cost = m_horsemanPrefab.GetComponent<UnitScript>().GetCost();
		if (!m_busy && money > cost)
		{
			m_busy = true;
			m_unitTraining = m_horsemanPrefab;
			m_unitScript = m_unitTraining.GetComponent<UnitScript>();
			return m_unitScript.GetCost();
		}
		return 0;
	}
}
