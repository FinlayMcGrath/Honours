using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneticAIScript : PlayerScript
{
	public bool m_won = false;
	public float m_timeTaken = 0, m_opponentHealth = 0, m_damageDealt = 0, m_moneyFloated = 0;
	public List<Choice> m_actionList = new List<Choice>();
	public bool m_firstTime = true;
	int m_numActions;
    int m_currentAction = 0;	

	// Use this for initialization
	void Start ()
	{
		if (m_firstTime)
		{
			Init();
		}

		InitialiseVariables();
		for (int i = 0; i < m_startingWorkers; i++)
        {
            GameObject worker = Instantiate(m_worker) as GameObject;
            worker.transform.parent = transform;
            m_numWorkers++;
        }

		AddWaypoints();

		m_currentAction = 0;

		if (m_firstTime)
		{
			Populate();
		}
	}

	public void Init()
	{
		m_actionList = new List<Choice>();
		m_numActions = 5000;
	}

	void Awake()
	{
		//create headquarters
		GameObject hq = Instantiate(m_hq) as GameObject;
		hq.transform.parent = transform;
		m_hq = hq;
		m_hq.tag = tag;
		m_hqScript = m_hq.GetComponent<HQScript>();
		m_hq.transform.position = new Vector3(0, -580, 0);
		m_hq.layer = gameObject.layer;
	}

	// Update is called once per frame
	void Update ()
    {
		FindFreeBuildings();
		if (m_currentAction >= m_actionList.Count)
		{
			m_actionList.Add((Choice)Random.Range(0, m_numChoices));
			m_numActions++;
        }

		//Select Action worker, Barracks or warrior        
		m_choice = m_actionList[m_currentAction];
        DoAction();
        m_currentAction++;

		//Record the action in order and in number

		CalculateMoney();
		m_moneyFloated += m_money;
		m_timeTaken += Time.deltaTime;
    }

    public void Populate()
    {
        for (int i = 0; i < m_numActions; i++)
        {
            m_actionList.Add((Choice)Random.Range(0, m_numChoices));
        }
    }
}
