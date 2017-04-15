using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordsmanScript : UnitScript
{
	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_damage = 20;
		m_health = 100;
		m_cost = 50;
		m_speed = 10;
		m_trainingTime = 5;
		m_unitType = UnitType.infantry;
		m_bonusDamage = UnitType.infantry;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
