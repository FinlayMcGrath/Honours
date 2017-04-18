using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
	public GameObject m_worker, m_warrior, m_archer, m_hq, m_barracksPrefab, m_archeryRangePrefab, m_stablesPrefab, m_unitSpawnPoint, m_leftWaypoint, m_rightWaypoint;
	public List<GameObject> m_barracks, m_stables, m_archeryRange;
	public int m_startingWorkers, m_numChoices, m_numWorkers, m_resourcePoints;
	public enum Choice {none, worker, barracks, warrior, archer, archeryRange, knight, stables, tower, left, right, tech, swordsman, spearman, crossbowman, horseman, catapult, wall, expansion, swordTech,
						spearTech, catapultTech, knightTech, crossbowTech};
	public enum Direction { left, right };
	public Direction m_direction;
	public bool m_tech, m_knightTech, m_crossbowTech, m_spearTech, m_swordTech, m_catapultTech;

	protected BarracksScript m_currentBarracks;
	protected ArcheryRangeScript m_currentArcheryRange;
	protected StablesScript m_currentStables;
	protected Choice m_choice;
	protected float m_money = 0, m_techCost;
    protected HQScript m_hqScript;
	protected int m_numWarriors = 0, m_workersPerResourcePoint;
	public GameObject m_initialWaypoint;
	protected List<WaypointScript> m_waypoints;
	protected bool m_hasBarracks, m_hasArcheryRange, m_hasStables, m_freeBarracks, m_freeArcheryRange, m_freeStables;

	// Use this for initialization
	void Start ()
    {
		InitialiseVariables();

        for (int i = 0; i < m_startingWorkers; i++)
        {
            GameObject worker = Instantiate(m_worker) as GameObject;
            worker.transform.parent = transform;
            m_numWorkers++;
        }

		AddWaypoints();


		//create headquarters
		GameObject hq = Instantiate(m_hq) as GameObject;
		hq.transform.parent = transform;
		m_hq = hq;
		m_hq.tag = tag;
		m_hqScript = m_hq.GetComponent<HQScript>();
		m_hq.transform.position = new Vector3(0, 580, 0);
		m_hq.layer = gameObject.layer;

		m_unitSpawnPoint = m_hq;

		
	}

	protected void InitialiseVariables()
	{
		//initialise some variables
		m_numChoices = 23;

		//buildings
		m_hasArcheryRange = false;
		m_hasBarracks = false;
		m_hasStables = false;
		m_freeBarracks = false;
		m_freeArcheryRange = false;
		m_freeStables = false;

		//tech
		m_tech = false;
		m_knightTech = false;
		m_crossbowTech = false;
		m_spearTech = false;
		m_swordTech = false;
		m_catapultTech = false;
		m_techCost = 100;

		//workers
		m_numWorkers = 0;
		m_resourcePoints = 1;
		m_workersPerResourcePoint = 20;
		m_startingWorkers = 10;
	}
	
	public void AddWaypoints()
	{
		//get all waypoints
		List<GameObject> waypoints = new List<GameObject>();
		waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));

		//find the initial two waypoints
		foreach (GameObject waypoint in waypoints)
		{
			if (waypoint.tag != transform.tag)
			{
				if (transform.tag == "Rule AI")
				{
					if (waypoint.GetComponent<WaypointScript>().ruleOrder == 1)
					{
						if (waypoint.transform.position.x < m_hq.transform.position.x)
						{
							m_leftWaypoint = waypoint;
                        }
						else
						{
							m_rightWaypoint = waypoint;
						}
					}
				}
				else
				{
					if (waypoint.GetComponent<WaypointScript>().geneticOrder == 1)
					{
						if (waypoint.transform.position.x < m_hq.transform.position.x)
						{
							m_leftWaypoint = waypoint;
						}
						else
						{
							m_rightWaypoint = waypoint;
						}
					}
				}
			}
		}

		m_initialWaypoint = m_leftWaypoint;
		m_direction = Direction.left;
    }

	// Update is called once per frame
	void Update ()
    {
		FindFreeBuildings();
        DecideAction();
        DoAction();
		CalculateMoney();
    }

	protected void CalculateMoney()
	{
		if (m_numWorkers < m_resourcePoints * m_workersPerResourcePoint)
		{
			m_money += m_numWorkers * Time.deltaTime * 10;
		}
		else
		{
			m_money += m_resourcePoints * m_workersPerResourcePoint * Time.deltaTime * 10;
		}
	}

    public int GetMoney()
    {
        return (int)m_money;
    }

    public int GetNumWorkers()
    {
		return transform.GetComponentsInChildren<WorkerScript>().Length;
	}

	public int GetNumWarriors()
	{
		return transform.GetComponentsInChildren<WarriorScript>().Length;
	}

	public int GetNumArchers()
	{
		return transform.GetComponentsInChildren<ArcherScript>().Length;
	}

	protected void BuildWorker()
    {
        m_money -= m_hqScript.BuildWorker(m_money);
    }

	protected void BuildBarracks()
	{
		if (m_money > m_barracksPrefab.GetComponent<BarracksScript>().m_cost)
		{
			GameObject barracks = Instantiate(m_barracksPrefab) as GameObject;
			barracks.transform.parent = transform;
			barracks.tag = tag;
			barracks.GetComponent<BarracksScript>().m_unitSpawner = m_unitSpawnPoint;
			barracks.GetComponent<BarracksScript>().m_initialTarget = m_initialWaypoint;
			barracks.transform.position = m_hq.transform.position;
			barracks.layer = gameObject.layer;
			m_hasBarracks = true;
			m_barracks.Add(barracks);
			m_money -= barracks.GetComponent<BarracksScript>().m_cost;
		}
	}

	protected void BuildArcheryRange()
	{
		if (m_money > m_archeryRangePrefab.GetComponent<ArcheryRangeScript>().m_cost)
		{
			GameObject archeryRange = Instantiate(m_archeryRangePrefab) as GameObject;
			archeryRange.transform.parent = transform;
			archeryRange.tag = tag;
			archeryRange.GetComponent<ArcheryRangeScript>().m_unitSpawner = m_unitSpawnPoint;
			archeryRange.GetComponent<ArcheryRangeScript>().m_initialTarget = m_initialWaypoint;
			archeryRange.transform.position = m_hq.transform.position;
			archeryRange.layer = gameObject.layer;
			m_hasArcheryRange = true;
			m_archeryRange.Add(archeryRange);
			m_money -= archeryRange.GetComponent<ArcheryRangeScript>().m_cost;
		}
	}

	protected void BuildStables()
	{
		if (m_money > m_stablesPrefab.GetComponent<StablesScript>().m_cost)
		{
			GameObject stables = Instantiate(m_stablesPrefab) as GameObject;
			stables.transform.parent = transform;
			stables.tag = tag;
			stables.GetComponent<StablesScript>().m_unitSpawner = m_unitSpawnPoint;
			stables.GetComponent<StablesScript>().m_initialTarget = m_initialWaypoint;
			stables.transform.position = m_hq.transform.position;
			stables.layer = gameObject.layer;
			m_hasStables = true;
			m_stables.Add(stables);
			m_money -= stables.GetComponent<StablesScript>().m_cost;
		}
	}

	protected void BuildTower()
	{
		TowerBaseScript towerbase = GetComponentInChildren<TowerBaseScript>();
		if (towerbase)
		{
			if (m_money > towerbase.m_cost)
			{
				m_money -= towerbase.m_cost;
				towerbase.Build();
			}
		}
	}

	protected void BuildWall()
	{
		WallPointScript wallpoint = GetComponentInChildren<WallPointScript>();
		if (wallpoint)
		{
			if (m_money > wallpoint.m_cost)
			{
				m_money -= wallpoint.m_cost;
				wallpoint.Build();
			}
		}
	}

	protected void BuildWarrior()
	{
		if (m_freeBarracks)
        {
			float cost = m_currentBarracks.GetComponent<BarracksScript>().BuildWarrior(m_money);
			if (m_money > cost && cost != 0)
			{
				m_money -= cost;
			}
		}
	}

	protected void BuildSwordsman()
	{
		if (m_freeBarracks && m_swordTech)
		{ 
			float cost = m_currentBarracks.GetComponent<BarracksScript>().BuildSwordsman(m_money);
			if (m_money > cost && cost != 0)
			{
				m_money -= cost;
			}
		}
	}

	protected void BuildSpearman()
	{
		if (m_freeBarracks && m_spearTech)
		{
			float cost = m_currentBarracks.GetComponent<BarracksScript>().BuildSpearman(m_money);
			if (m_money > cost && cost != 0)
			{
				m_money -= cost;
			}
		}
	}

	protected void BuildArcher()
	{
		if (m_freeArcheryRange)
		{
			float cost = m_currentArcheryRange.GetComponent<ArcheryRangeScript>().BuildArcher(m_money);
			if (m_money > cost && cost != 0)
			{
				m_money -= cost;
			}
		}
	}

	protected void BuildCrossbowman()
	{
		if (m_freeArcheryRange && m_crossbowTech)
		{
			float cost = m_currentArcheryRange.GetComponent<ArcheryRangeScript>().BuildCrossbowman(m_money);
			if (m_money > cost && cost != 0)
			{
				m_money -= cost;
			}
		}
	}

	protected void BuildCatapult()
	{
		if (m_freeArcheryRange && m_catapultTech)
		{
			float cost = m_currentArcheryRange.GetComponent<ArcheryRangeScript>().BuildCatapult(m_money);
			if (m_money > cost && cost != 0)
			{
				m_money -= cost;
			}
		}
	}

	protected void BuildKnight()
	{
		if (m_freeStables && m_knightTech)
		{
			float cost = m_currentStables.GetComponent<StablesScript>().BuildKnight(m_money);
			if (m_money > cost && cost != 0)
			{
				m_money -= cost;
			}
		}
	}

	protected void BuildHorseman()
	{
		if (m_freeStables)
		{
			float cost = m_currentStables.GetComponent<StablesScript>().BuildHorseman(m_money);
			if (m_money > cost && cost != 0)
			{
				m_money -= cost;
			}
		}
	}

	protected bool ResearchTech(bool tech)
	{
		if (m_money > m_techCost && !tech)
		{
			m_money -= m_techCost;
			return true;
		}
		return tech;
	}

	protected void BuildExpansion()
	{
		ExpansionPointScript expansion = gameObject.GetComponentInChildren<ExpansionPointScript>();
		if (expansion)
		{
			if (m_money > expansion.m_cost)
			{
				m_money -= expansion.m_cost;
				expansion.Build();
				m_resourcePoints++;
			}
		}
	}

	protected void FindFreeBuildings()
	{
		m_currentBarracks = null;
		m_currentArcheryRange = null;
		m_currentStables = null;
		m_freeBarracks = false;
		m_freeArcheryRange = false;
		m_freeStables = false;

		//find free buildings
		foreach (GameObject barracks in m_barracks)
		{
			if (!barracks.GetComponent<BarracksScript>().GetBusy() && barracks.activeSelf)
			{
				m_currentBarracks = barracks.GetComponent<BarracksScript>();
				m_freeBarracks = true;
				break;
			}
		}

		foreach (GameObject archeryRange in m_archeryRange)
		{
			if (!archeryRange.GetComponent<ArcheryRangeScript>().GetBusy() && archeryRange.activeSelf)
			{
				m_currentArcheryRange = archeryRange.GetComponent<ArcheryRangeScript>();
				m_freeArcheryRange = true;
				break;
			}
		}

		foreach (GameObject stables in m_stables)
		{
			if (!stables.GetComponent<StablesScript>().GetBusy() && stables.activeSelf)
			{
				m_currentStables = stables.GetComponent<StablesScript>();
				m_freeStables = true;
				break;
			}
		}
	}

	private void DecideAction()
    {
		//build workers if less than 10
		if (m_numWorkers < 10 && !m_hqScript.GetBusy())
		{
			m_choice = Choice.worker;
		}
		else if (GetComponentInChildren<ExpansionPointScript>() != null && m_resourcePoints < 3 && m_numWorkers == m_resourcePoints * m_workersPerResourcePoint)
		{
			m_choice = Choice.expansion;
		}
		else if (!m_freeStables && m_tech && m_hasArcheryRange && !m_hasStables)
		{
			m_choice = Choice.stables;
		}
		else if (!m_freeArcheryRange && m_tech && m_hasBarracks && !m_hasArcheryRange)
		{
			m_choice = Choice.archeryRange;
		}
		//build archer if archery range isn't busy
		else if (m_freeArcheryRange && m_money > m_currentArcheryRange.m_archerPrefab.GetComponent<ArcherScript>().m_cost)
		{
			m_choice = Choice.archer;
		}
		//build warrior if barracks isn't busy
		else if (m_freeBarracks && m_money > m_currentBarracks.m_warriorPrefab.GetComponent<WarriorScript>().m_cost)
		{
			m_choice = Choice.warrior;
		}
		//keep building workers if below the maximum effectiveness
		else if (m_numWorkers < m_resourcePoints * m_workersPerResourcePoint && !m_hqScript.GetBusy())
		{
			m_choice = Choice.worker;
		}
		//build knight if stables isn't busy
		else if (m_freeStables && m_knightTech && m_money > m_currentStables.m_knightPrefab.GetComponent<KnightScript>().m_cost)
		{
			m_choice = Choice.knight;
		}
		//build horseman if stables isn't busy
		else if (m_freeStables == true && m_money > m_currentStables.m_horsemanPrefab.GetComponent<HorsemanScript>().m_cost)
		{
			m_choice = Choice.horseman;
		}
		//build crossbowman if archery range isn't busy
		else if (m_freeArcheryRange && m_crossbowTech && m_money > m_currentArcheryRange.m_crossbowmanPrefab.GetComponent<CrossbowmanScript>().m_cost)
		{
			m_choice = Choice.crossbowman;
		}
		//build catapult if archery range isn't busy
		else if (m_freeArcheryRange && m_catapultTech && m_money > m_currentArcheryRange.m_catapultPrefab.GetComponent<CatapultScript>().m_cost)
		{
			m_choice = Choice.catapult;
		}
		//build spearman if barracks isn't busy
		else if (m_freeBarracks && m_spearTech && m_money > m_currentBarracks.m_spearmanPrefab.GetComponent<SpearmanScript>().m_cost)
		{
			m_choice = Choice.spearman;
		}
		//build swordsman if barracks isn't busy
		else if (m_freeBarracks && m_swordTech && m_money > m_currentBarracks.m_swordsmanPrefab.GetComponent<SwordsmanScript>().m_cost)
		{
			m_choice = Choice.swordsman;
		}
		//tech to unlock advanced stuff
		else if (!m_tech && m_money > m_techCost)
		{
			m_choice = Choice.tech;
		}
		//build stables if there are no free stables and an archery range has been built
		else if (!m_freeStables && m_hasArcheryRange && m_money > m_stablesPrefab.GetComponent<StablesScript>().m_cost)
		{
			m_choice = Choice.stables;
		}
		//build archery range if there are no free archery ranges and a barracks has been built
		else if (!m_freeArcheryRange && m_hasBarracks && m_money > m_archeryRangePrefab.GetComponent<ArcheryRangeScript>().m_cost && m_archeryRange.Count < 3)
		{
			m_choice = Choice.archeryRange;
		}
		//build barracks when it can be afforded and there are no free barracks
		else if (!m_freeBarracks && m_money > m_barracksPrefab.GetComponent<BarracksScript>().m_cost && m_barracks.Count < 3)
		{
			m_choice = Choice.barracks;
		}
		else if (!m_swordTech && m_tech && m_money > m_techCost)
		{
			m_choice = Choice.swordTech;
		}
		else if (!m_spearTech && m_money > m_techCost)
		{
			m_choice = Choice.spearTech;
		}
		else if (!m_crossbowTech && m_money > m_techCost)
		{
			m_choice = Choice.crossbowTech;
		}
		else if (!m_catapultTech && m_tech && m_money > m_techCost)
		{
			m_choice = Choice.catapultTech;
		}
		else if (!m_knightTech && m_tech && m_money > m_techCost)
		{
			m_choice = Choice.knightTech;
		}
		else if (GetComponentInChildren<TowerBaseScript>() != null)
		{
			m_choice = Choice.tower;
		}
		else if (GetComponentInChildren<TowerBaseScript>() != null)
		{
			m_choice = Choice.wall;
		}
	}

    protected void DoAction()
    {
        switch (m_choice)
        {
            case Choice.worker:
                BuildWorker();
                break;
            case Choice.warrior:
                BuildWarrior();
                break;
			case Choice.crossbowman:
				BuildCrossbowman();
				break;
			case Choice.catapult:
				BuildCatapult();
				break;
			case Choice.barracks:
				BuildBarracks();
				break;
			case Choice.archer:
				BuildArcher();
				break;
			case Choice.archeryRange:
				BuildArcheryRange();
				break;
			case Choice.knight:
				BuildKnight();
				break;
			case Choice.horseman:
				BuildHorseman();
				break;
			case Choice.expansion:
				BuildExpansion();
				break;
			case Choice.swordsman:
				BuildSwordsman();
				break;
			case Choice.spearman:
				BuildSpearman();
				break;
			case Choice.stables:
				BuildStables();
				break;
			case Choice.tower:
				BuildTower();
				break;
			case Choice.left:
				setDirection(m_leftWaypoint);
				break;
			case Choice.right:
				setDirection(m_rightWaypoint);
				break;
			case Choice.wall:
                BuildWall();
				break;
			case Choice.tech:
				m_tech = ResearchTech(m_tech);
				break;
			case Choice.swordTech:
				if (m_tech)
				{
					m_swordTech = ResearchTech(m_swordTech);
				}
				break;
			case Choice.spearTech:
				m_spearTech = ResearchTech(m_spearTech);
				break;
			case Choice.crossbowTech:
				m_crossbowTech = ResearchTech(m_crossbowTech);
				break;
			case Choice.knightTech:
				if (m_tech)
				{
					m_knightTech = ResearchTech(m_knightTech);
				}
				break;
			case Choice.catapultTech:
				if (m_tech)
				{
					m_catapultTech = ResearchTech(m_catapultTech);
				}
				break;
			case Choice.none:
                break;
        }
        m_choice = Choice.none;
    }

	void setDirection(GameObject waypoint)
	{
		m_initialWaypoint = waypoint;
		foreach (GameObject barracks in m_barracks)
		{
			barracks.GetComponent<BarracksScript>().m_initialTarget = m_initialWaypoint;
		}

		foreach (GameObject archeryRange in m_archeryRange)
		{
			archeryRange.GetComponent<ArcheryRangeScript>().m_initialTarget = m_initialWaypoint;
		}

		foreach (GameObject stables in m_stables)
		{
			stables.GetComponent<StablesScript>().m_initialTarget = m_initialWaypoint;
		}
	}

	public void SwapDirection()
	{
		if (m_initialWaypoint == m_leftWaypoint)
		{
			setDirection(m_rightWaypoint);
			m_direction = Direction.right;
		}
		else if (m_initialWaypoint == m_rightWaypoint)
		{
			setDirection(m_leftWaypoint);
			m_direction = Direction.left;
		}
	}
}
