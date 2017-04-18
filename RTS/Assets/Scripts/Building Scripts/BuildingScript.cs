using UnityEngine;
using System.Collections;


public class BuildingScript : MonoBehaviour
{
	public int m_cost = 0;
	public float m_health;
	public GameObject m_unitSpawner;
	public GameObject m_initialTarget;

	public bool m_busy = false;
    protected bool m_trainedUnit = false;
    protected UnitScript m_unitScript;
    protected GameObject m_unitTraining;
	protected float m_gameSpeed = 1, m_buildingTime, m_trainingTime, m_timeBuilding;
	
	// Update is called once per frame
	void Update()
	{
		if (!gameObject.activeSelf)
		{
			m_timeBuilding += Time.deltaTime;
			if (m_timeBuilding > m_buildingTime)
			{
				gameObject.SetActive(true);
			}
		}
		if (m_busy)
		{
			m_trainingTime += Time.deltaTime;
			if (m_trainingTime > m_unitScript.GetTrainingTime())
			{
				m_trainingTime = 0;
				m_busy = false;
				GameObject unit = Instantiate(m_unitTraining) as GameObject;
				if (transform.parent)
				{
					unit.transform.parent = transform.parent;
					unit.tag = transform.parent.tag;
					unit.transform.position = m_unitSpawner.transform.position;
					unit.layer = gameObject.layer;
					unit.GetComponent<UnitScript>().m_targetPosition = m_initialTarget;
                    if (tag == "Rule AI")
					{
						transform.parent.GetComponent<PlayerScript>().SwapDirection();
					}
				}
			}
		}
	}

	public GameObject GetTrainedUnit()
    {
        if (m_trainedUnit)
        {
            m_trainedUnit = false;
            return m_unitTraining;
        }
        return null;
    }

    public bool GetBusy()
    {
        return m_busy;
    }

    public bool IsUnitTrained()
    {
        if (m_trainedUnit)
        {
            m_trainedUnit = false;
            m_unitTraining = null;
            return true;
        }
        return false;
    }
}
