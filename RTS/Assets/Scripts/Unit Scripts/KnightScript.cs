using UnityEngine;
using System.Collections;

public class KnightScript : UnitScript {

	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_damage = 20;
		m_health = 100;
		m_speed = 20;
		m_cost = 70;
		m_trainingTime = 5;
		m_unitType = UnitType.cavalry;
		m_bonusDamage = UnitType.none;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
