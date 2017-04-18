using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointScript : MonoBehaviour
{

	public int ruleOrder, geneticOrder;
	public WaypointScript m_nextRuleWaypoint, m_nextGeneticWaypoint, m_secondWaypoint;
	public bool m_useSecond;

	public List<UnitScript> m_units;
	
	// Use this for initialization
	void Start ()
	{
		m_units = new List<UnitScript>();
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!col.isTrigger)
		{
			if (m_useSecond && col.transform.parent.GetComponent<PlayerScript>().m_direction == PlayerScript.Direction.left)
			{
				if (tag == "Rule AI" && col.tag == "Rule AI")
				{
					col.GetComponent<UnitScript>().m_targetPosition = m_nextRuleWaypoint.gameObject;
					m_units.Add(col.GetComponent<UnitScript>());
				}
				else if (tag == "Genetic AI" && col.tag == "Genetic AI")
				{
					col.GetComponent<UnitScript>().m_targetPosition = m_nextGeneticWaypoint.gameObject;
					m_units.Add(col.GetComponent<UnitScript>());
				}
			}
			else if (m_useSecond && col.transform.parent.GetComponent<PlayerScript>().m_direction == PlayerScript.Direction.right)
			{
				if (col.transform.tag == tag)
				{
					col.GetComponent<UnitScript>().m_targetPosition = m_secondWaypoint.gameObject;
					m_units.Add(col.GetComponent<UnitScript>());
				}
			}
			if (!m_useSecond || col.transform.tag != tag)
			{
				if (col.transform.tag == "Rule AI")
				{
					col.GetComponent<UnitScript>().m_targetPosition = m_nextRuleWaypoint.gameObject;
					m_units.Add(col.GetComponent<UnitScript>());
				}
				if (col.transform.tag == "Genetic AI")
				{
					col.GetComponent<UnitScript>().m_targetPosition = m_nextGeneticWaypoint.gameObject;
					m_units.Add(col.GetComponent<UnitScript>());
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		m_units.Remove(col.GetComponent<UnitScript>());
	}
}
