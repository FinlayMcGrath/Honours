using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitScript : MonoBehaviour
{
	public enum Direction { left, right };
	public double m_trainingTime = 5;
    public int m_cost = 20;
    public float m_health;
    public float m_damage;
	public float m_speed = 5;
	public GameObject m_targetPosition;
	public bool m_deleteFlag;

	protected enum UnitType { none, infantry, ranged, cavalry, siege, civilian, building };
	protected List<UnitScript> m_nearbyEnemies;
	protected List<BuildingScript> m_nearbyEnemyBuildings;
	protected BoxCollider2D m_collider;
	protected UnitType m_unitType;
	protected UnitType m_bonusDamage = UnitType.none;

	float m_bonusModifer = 0.5f;

	// Use this for initialization
	public virtual void Start ()
	{
		m_collider = GetComponent<BoxCollider2D>();
		m_nearbyEnemies = new List<UnitScript>();
		m_nearbyEnemyBuildings = new List<BuildingScript>();
	}

	// Update is called once per frame
	public virtual void Update ()
    {
		UpdateLists();
		if (!DamagePhase())
		{
			Move();
		}
    }

	void UpdateLists()
	{
		//check that enemies still exist
		for (int i = 0; i < m_nearbyEnemies.Count; i++)
		{
			if (m_nearbyEnemies[i] == null)
			{
				m_nearbyEnemies.Remove(m_nearbyEnemies[i]);
			}
		}
		//check that enemies still exist
		for (int i = 0; i < m_nearbyEnemyBuildings.Count; i++)
		{
			if (m_nearbyEnemyBuildings[i] == null)
			{
				m_nearbyEnemyBuildings.Remove(m_nearbyEnemyBuildings[i]);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//if enemy unit add to collision list
		if (col.transform.tag != transform.tag && !col.isTrigger)
		{
			if (col.transform.GetComponent<UnitScript>() != null)
			{
				m_nearbyEnemies.Add(col.transform.GetComponent<UnitScript>());
			}
			else if(col.transform.GetComponent<BuildingScript>() != null)
            {
				m_nearbyEnemyBuildings.Add(col.transform.GetComponent<BuildingScript>());
			}
		}		
	}

	void OnTriggerExit2D(Collider2D col)
	{
		m_nearbyEnemies.Remove(col.transform.GetComponent<UnitScript>());
		m_nearbyEnemyBuildings.Remove(col.transform.GetComponent<BuildingScript>());
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		
	}

    public double GetTrainingTime()
    {
        return m_trainingTime;
    }

    public int GetCost()
    {
        return m_cost;
    }

	protected void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, m_targetPosition.transform.position, m_speed * Time.deltaTime);
	}

	void AttackEnemies()
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
		}

		//calculate damage
		float damageValue = 0;
		if (m_bonusDamage == target.m_bonusDamage)
		{
			damageValue = m_damage * Time.deltaTime * m_bonusModifer;
		}
		damageValue += m_damage * Time.deltaTime;

		//if unit belongs to genetic ai, let ai know damage value
		if (tag == "Genetic AI")
		{
			GetComponentInParent<GeneticAIScript>().m_damageDealt += damageValue;
		}

		//deal damage to it
		target.m_health -= damageValue;

		//if the hit enemy is dead flag for deletion
		if (target.m_health <= 0)
		{
			target.m_deleteFlag = true;
			m_nearbyEnemies.Remove(target);
		}
	}

	void AttackBuildings()
	{
		if (m_nearbyEnemyBuildings[0] != null)
		{
			//find the closest enemy
			float distance = (m_nearbyEnemyBuildings[0].transform.position - transform.position).magnitude;

			BuildingScript target = m_nearbyEnemyBuildings[0];

			foreach (BuildingScript building in m_nearbyEnemyBuildings)
			{
				float tempDistance = (building.transform.position - transform.position).magnitude;
				if (tempDistance < distance)
				{
					distance = tempDistance;
					target = building;
				}
			}

			if (m_bonusDamage == UnitType.building)
			{
				//deal bonus damage to it
				target.m_health -= m_damage * Time.deltaTime * m_bonusModifer;
			}

			//deal damage to it
			target.m_health -= m_damage * Time.deltaTime;			
		}
	}

	protected bool DamagePhase()
	{
		//if there are any nearby enemies
		if (m_nearbyEnemies.Count > 0)
		{
			AttackEnemies();
			return true;
		}
		//if there are any nearby enemy buildings
		else if (m_nearbyEnemyBuildings.Count > 0)
		{
			AttackBuildings();
			return true;
		}

		//return false if hasn't attacked
		return false;
	}
}
