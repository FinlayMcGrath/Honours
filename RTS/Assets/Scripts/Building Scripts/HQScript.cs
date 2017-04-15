using UnityEngine;
using System.Collections;

public class HQScript : BuildingScript
{
	public GameObject m_worker;
    // Use this for initialization
    void Start ()
    {
        m_health = 5000;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_busy)
        {
			m_trainingTime += Time.deltaTime;
            if (m_trainingTime > m_unitScript.GetTrainingTime())
            {
                m_trainingTime = 0;
                m_busy = false;
				GameObject worker = Instantiate(m_worker) as GameObject;
				if (transform.parent)
				{
					worker.transform.parent = transform.parent;
					worker.tag = transform.parent.tag;
					transform.parent.GetComponent<PlayerScript>().m_numWorkers++;
				}
			}
        }
    }
	

	//if HQ isn't busy, start building a worker and return the cost of the worker
	public int BuildWorker(float money)
    {
        int cost = m_worker.GetComponent<WorkerScript>().GetCost();
        if (!m_busy && money > cost)
        {
            m_busy = true;
            m_unitTraining = m_worker;
            m_unitScript = m_unitTraining.GetComponent<WorkerScript>();
            return m_unitScript.GetCost();
        }
        return 0;
    }    
}
