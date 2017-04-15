using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerScript : BuildingScript
{
	public GameObject m_towerBasePrefab;
	List<UnitScript> m_nearbyEnemies;
    float m_damage;

	// Use this for initialization
	void Start ()
	{
		m_nearbyEnemies = new List<UnitScript>();
		m_health = 1000;
		m_damage = 10;
	}
	
	// Update is called once per frame
	void Update ()
	{
		DamagePhase();
		if (m_health <= 0)
		{
			GameObject towerBase = Instantiate(m_towerBasePrefab) as GameObject;
			towerBase.transform.position = transform.position;
			towerBase.transform.parent = GameObject.FindGameObjectWithTag("Map").transform;
			Destroy(gameObject);
		}
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		//if enemy unit add to collision list
		if (col.transform.tag != transform.tag)
		{
			if (col.transform.GetComponent<UnitScript>() != null)
			{
				m_nearbyEnemies.Add(col.transform.GetComponent<UnitScript>());
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		m_nearbyEnemies.Remove(col.transform.GetComponent<UnitScript>());
	}

	void DamagePhase()
	{
		//check that enemies still exist
		while (m_nearbyEnemies.Count > 0 && m_nearbyEnemies[0] == null)
		{
			m_nearbyEnemies.Remove(m_nearbyEnemies[0]);
		}

		//if there are any nearby enemies
		if (m_nearbyEnemies.Count > 0)
		{
			//find the closest enemy
			float distance = (m_nearbyEnemies[0].transform.position - transform.position).magnitude;
			UnitScript target = m_nearbyEnemies[0];
			foreach (UnitScript unit in m_nearbyEnemies)
			{
				if (unit != null)
				{
					float tempDistance = (unit.transform.position - transform.position).magnitude;
					if (tempDistance < distance)
					{
						distance = tempDistance;
						target = unit;
					}
				}
				else
				{
					//m_nearbyEnemies.Remove(unit);
				}
			}
			//deal damage to it
			target.m_health -= m_damage * Time.deltaTime;
			//if the hit enemy is dead flag for deletion
			if (target.m_health <= 0)
			{
				target.m_deleteFlag = true;
				m_nearbyEnemies.Remove(target);
			}
		}
	}
}
