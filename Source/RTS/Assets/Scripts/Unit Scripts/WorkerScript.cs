using UnityEngine;
using System.Collections;

public class WorkerScript : UnitScript
{

	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_damage = 0;
		m_health = 10;
		m_speed = 15;
		m_cost = 20;
		m_trainingTime = 2.5;
		m_unitType = UnitType.civilian;
		m_bonusDamage = UnitType.none;
	}

	// Update is called once per frame
	public override void Update()
	{
		//base.Update();
	}
}
