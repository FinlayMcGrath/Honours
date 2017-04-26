using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class EvolutionScript : MonoBehaviour
{
    public GameObject m_geneticAIPrefab, m_ruleAIPrefab, m_canvas, m_gameControllerPrefab, m_wayPointController;
    List<GameObject> m_population;
	List<float> m_fitnessFactor, m_parentFitness, m_winRate;
	GameObject m_ruleAI, m_gameController;
    int m_populationSize = 10, m_currentAttempt = 0, m_numIterations = 0, maxIterations = 100;

	// Use this for initialization
	void Start ()
	{
		m_parentFitness = new List<float>();
		m_winRate = new List<float>();
		Initialise();
		Evaluate();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //either hq has died move to next game
        if (m_gameController.GetComponent<GameControllerScript>().isGameOver())
        {
            //Destroy old game objects
            Destroy(m_ruleAI);
            Destroy(m_gameController);
			m_population[m_currentAttempt].SetActive(false);
            m_currentAttempt++;
			if (m_currentAttempt < m_populationSize)
			{
				Evaluate();
			}
			else
			{
				m_currentAttempt = 0;
				m_numIterations++;
				Select();
				Resources.UnloadUnusedAssets();
				Debug.Log(m_numIterations);
			}
        }
		if (m_numIterations == maxIterations)
		{
			Debug.Log("Fitness = " + string.Join("\n", new List<float>(m_parentFitness).ConvertAll(i => i.ToString()).ToArray()));
			Debug.Log("Win Rate = " + string.Join("\n", new List<float>(m_winRate).ConvertAll(i => i.ToString()).ToArray()));
			m_numIterations = 0;
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
			System.GC.Collect();
            Resources.UnloadUnusedAssets();
		}
	}

	//Initialise
	void Initialise()
    {
		m_population = new List<GameObject>();
		m_fitnessFactor = new List<float>();

		

		for (int i = 0; i < m_populationSize; i++)
        {
            GameObject geneticAI = Instantiate(m_geneticAIPrefab) as GameObject;
			geneticAI.SetActive(false);
            m_population.Add(geneticAI);
			m_fitnessFactor.Add(0);
        }
	}

    //Evaluate (Gameplay)
    void Evaluate()
	{
		m_gameController = Instantiate(m_gameControllerPrefab) as GameObject;

        //m_population[m_currentAttempt] = Instantiate(m_population[m_currentAttempt], m_population[m_currentAttempt].transform.position, m_population[m_currentAttempt].transform.rotation) as GameObject;
		m_population[m_currentAttempt].SetActive(true);

        m_ruleAI = Instantiate(m_ruleAIPrefab, m_ruleAIPrefab.transform.position, m_ruleAIPrefab.transform.rotation) as GameObject;

        m_gameController.GetComponent<GameControllerScript>().m_geneticAI = m_population[m_currentAttempt];
        m_gameController.GetComponent<GameControllerScript>().m_ruleAI = m_ruleAI;

		//get the waypoints 
		//m_ruleAI.GetComponent<PlayerScript>().AddWaypoints(m_wayPointController.GetComponent<WaypointControllerScript>().m_waypoints);
		//m_population[m_currentAttempt].GetComponent<PlayerScript>().AddWaypoints(m_wayPointController.GetComponent<WaypointControllerScript>().m_waypoints);
	}

    //Select (fitness function)

	void Select()
	{
		//fitness function has several factors
		//1. the best AI will have won
		//2. in the shortest time
		//3. dealing most damage
		int wincount = 0;
		//if an AI won, make make it eligible for breeding
		for (int i = 0; i < m_population.Count; i++)
		{
			//temp variable so don't need keep referencing
			GeneticAIScript ai = m_population[i].GetComponent<GeneticAIScript>();
            if (ai.m_won)
			{ 
				//big number to represent that winning is most important
				m_fitnessFactor[i] += 10000;
							
				//low time is good				 
				m_fitnessFactor[i] -= ai.m_timeTaken;

				wincount++;
            }
			else
			{
				//high time is good				 
				m_fitnessFactor[i] += ai.m_timeTaken;
			}

			//having high damage per second is good
			m_fitnessFactor[i] += ai.m_damageDealt / ai.m_timeTaken;

			//dealing damage to opponent hq is good
			m_fitnessFactor[i] -= ai.m_opponentHealth;

			//having low wasted resources is good
			//calculated if they have more resources than they can spend
			//idea is that if they are saving up a lot of money, its being spent inefficiently
			if (ai.m_moneyFloated / ai.m_actionList.Count > 300)
			{
				m_fitnessFactor[i] -= (ai.m_moneyFloated / ai.m_actionList.Count - 300);
			}
		}

		int first = 0;
		int second = 0;
		for (int i = 0; i < m_population.Count; i++)
		{
			if (m_fitnessFactor[i] > m_fitnessFactor[first])
			{
				second = first;
				first = i;
			}
			else if (m_fitnessFactor[i] > m_fitnessFactor[second])
            {
				second = i;
			}
        }

		m_parentFitness.Add(m_fitnessFactor[first]);
		m_parentFitness.Add(m_fitnessFactor[second]);
		m_winRate.Add(wincount / m_populationSize);
		

		Breed(m_population[first].GetComponent<GeneticAIScript>(), m_population[second].GetComponent<GeneticAIScript>());

	}

    //Breed

	void Breed(GeneticAIScript parent1, GeneticAIScript parent2)
	{
		for (int i = 0; i < m_population.Count; i++)
		{
			Destroy(m_population[i]);
		}
		Initialise();
		for (int i = 0; i < m_populationSize; i++)
		{
			int choice, numActions;
			GameObject breederObject = Instantiate(m_geneticAIPrefab) as GameObject;
			GeneticAIScript breeder = breederObject.GetComponent<GeneticAIScript>();
			breeder.Init();
			numActions = parent1.m_actionList.Count;
            if (parent2.m_actionList.Count > numActions)
			{
				numActions = parent2.m_actionList.Count;
            }

			for (int j = 0; j < numActions; j++)
			{
				if (j % 2 == 0 && parent1.m_actionList.Count >= numActions)
				{ 
					if (j < breeder.m_actionList.Count)
					{
						breeder.m_actionList[j] = parent1.m_actionList[j];
					}
					else
					{
						breeder.m_actionList.Add(parent1.m_actionList[j]);
					}
                }
				else if (parent2.m_actionList.Count >= numActions)
				{
					if (j < breeder.m_actionList.Count)
					{
						breeder.m_actionList[j] = parent2.m_actionList[j];
					}
					else
					{
						breeder.m_actionList.Add(parent2.m_actionList[j]);
					}
				}

				if (j % 2 != 0 && parent2.m_actionList.Count >= numActions)
				{
					if (j < breeder.m_actionList.Count)
					{
						breeder.m_actionList[j] = parent2.m_actionList[j];
					}
					else
					{
						breeder.m_actionList.Add(parent2.m_actionList[j]);
					}
				}
				else if (parent1.m_actionList.Count >= numActions)
				{
					if (j < breeder.m_actionList.Count)
					{
						breeder.m_actionList[j] = parent1.m_actionList[j];
					}
					else
					{
						breeder.m_actionList.Add(parent1.m_actionList[j]);
					}
				}
            }
			breeder = Mutate(breeder);

			//update the component

			m_population[i].GetComponent<GeneticAIScript>().m_firstTime = false;
			m_population[i].GetComponent<GeneticAIScript>().m_actionList = breeder.m_actionList;
			Destroy(breederObject);
			Destroy(breeder);
		}
		Evaluate();
	}

    //Mutate
	GeneticAIScript Mutate(GeneticAIScript child)
	{
		//the % of actions to be randomised
		double mutationPercent = 2;

		//randomly determine which actions will be randomised
		for (int i = 0; i < child.m_actionList.Count; i++)
		{
			if (Random.Range(1, 100) <= mutationPercent)
			{
				child.m_actionList[i] = (GeneticAIScript.Choice)Random.Range(1, child.m_numChoices);
            }
		}
		return child;
	}
}
