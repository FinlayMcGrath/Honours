using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class GameControllerScript : MonoBehaviour
{

	public GameObject m_ruleAI, m_geneticAI, m_mapPrefab;
	bool m_endGame = false;
	GameObject m_map;

	// Use this for initialization
	void Start ()
	{
		m_map = Instantiate(m_mapPrefab) as GameObject;
	}

	// Update is called once per frame
	void Update ()
	{
		//delete any dead units
		CleanupDeadUnits();

		//if an hq dies, end the game
		if (m_ruleAI.GetComponent<PlayerScript>().m_hq.GetComponent<HQScript>().m_health <= 0 || m_geneticAI.GetComponent<PlayerScript>().m_hq.GetComponent<HQScript>().m_health <= 0)
		{
			//end game     
			m_endGame = true;
			if (m_ruleAI.GetComponent<PlayerScript>().m_hq.GetComponent<HQScript>().m_health <= 0)
			{
				m_geneticAI.GetComponent<GeneticAIScript>().m_won = true;
            }


			EditorUtility.SetDirty(m_map);
			Destroy(m_map);
			m_map = null;
			//record enemy health
			m_geneticAI.GetComponent<GeneticAIScript>().m_opponentHealth = m_ruleAI.GetComponent<PlayerScript>().m_hq.GetComponent<HQScript>().m_health;
        }
	}

    public bool isGameOver()
    {
        return m_endGame;
    }

	void CleanupDeadUnits()
	{
		//get all the rule AI's units
		foreach (UnitScript unit in m_ruleAI.GetComponentsInChildren<UnitScript>())
		{
			if (unit.m_deleteFlag)
			{
				EditorUtility.SetDirty(unit.gameObject);
				Destroy(unit.gameObject);
			}
		}

		//get all the genetic AI's units
		foreach (UnitScript unit in m_geneticAI.GetComponentsInChildren<UnitScript>())
		{
			if (unit.m_deleteFlag)
			{
				EditorUtility.SetDirty(unit.gameObject);
				Destroy(unit.gameObject);
			}
		}
	}	
}
