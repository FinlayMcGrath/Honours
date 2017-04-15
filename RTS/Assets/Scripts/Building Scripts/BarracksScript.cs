using UnityEngine;
using System.Collections;

public class BarracksScript : BuildingScript
{
	public GameObject m_warriorPrefab, m_swordsmanPrefab, m_spearmanPrefab;

    // Use this for initialization
    void Start ()
    {
        m_cost = 50;
		m_buildingTime = 5;
		m_timeBuilding = 0;
		m_warriorPrefab.layer = gameObject.layer;
		m_swordsmanPrefab.layer = gameObject.layer;
		m_spearmanPrefab.layer = gameObject.layer;
	}

	//if barracks isn't busy, start building a warrior and return the cost of the warrior
	public int BuildWarrior(float money)
	{
		int cost = m_warriorPrefab.GetComponent<WarriorScript>().GetCost();
		if (!m_busy && money > cost)
		{
			m_busy = true;
			m_unitTraining = m_warriorPrefab;
			m_unitScript = m_unitTraining.GetComponent<WarriorScript>();
			m_unitScript.GetComponent<UnitScript>().m_targetPosition = m_initialTarget;
			return m_unitScript.GetCost();
		}
		return 0;
	}

	//if barracks isn't busy, start building a swordsman and return the cost of the swordsman
	public int BuildSwordsman(float money)
	{
		int cost = m_swordsmanPrefab.GetComponent<SwordsmanScript>().GetCost();
		if (!m_busy && money > cost)
		{
			m_busy = true;
			m_unitTraining = m_swordsmanPrefab;
			m_unitScript = m_unitTraining.GetComponent<SwordsmanScript>();
			m_unitScript.GetComponent<UnitScript>().m_targetPosition = m_initialTarget;
			return m_unitScript.GetCost();
		}
		return 0;
	}

	//if barracks isn't busy, start building a spearman and return the cost of the spearman
	public int BuildSpearman(float money)
	{
		int cost = m_spearmanPrefab.GetComponent<SpearmanScript>().GetCost();
		if (!m_busy && money > cost)
		{
			m_busy = true;
			m_unitTraining = m_spearmanPrefab;
			m_unitScript = m_unitTraining.GetComponent<SpearmanScript>();
			m_unitScript.GetComponent<UnitScript>().m_targetPosition = m_initialTarget;
			return m_unitScript.GetCost();
		}
		return 0;
	}
}
