using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBaseScript : MonoBehaviour
{
	public GameObject m_towerPrefab;
	public float m_cost;

	private List<GameObject> m_nearbyUnits;
	
	// Use this for initialization
	void Start ()
	{
		m_cost = 100;
		m_nearbyUnits = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
	{
		int rule = 0;
		int genetic = 0;
		Transform ruletransform = transform;
		Transform genetictransform = transform;

		foreach(GameObject gameObject in m_nearbyUnits)
		{
			if (gameObject != null)
			{
				if (gameObject.tag == "Rule AI")
				{
					rule++;
					ruletransform = gameObject.transform.parent.transform;
				}
				else if (gameObject.tag == "Genetic AI")
				{
					genetic++;
					genetictransform = gameObject.transform.parent.transform;
				}
			}
		}

		if (rule > genetic)
		{
			tag = "Rule AI";
			transform.parent = ruletransform;
        }
		else if (genetic > rule)
		{
			tag = "Genetic AI";
			transform.parent = genetictransform;
		}
		else
		{
			tag = "Untagged";
			transform.parent = GameObject.FindGameObjectWithTag("Map").transform;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.GetComponent<UnitScript>() != null)
		{
			m_nearbyUnits.Add(col.transform.gameObject);
		}
    }

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.transform.GetComponent<UnitScript>() != null)
		{
			m_nearbyUnits.Remove(col.transform.gameObject);
		}
    }

	public void Build()
	{
		GameObject tower = Instantiate(m_towerPrefab) as GameObject;
		tower.transform.position = transform.position;
		tower.tag = tag;
		tower.transform.parent = transform.parent;
		Destroy(this);
	}
}
