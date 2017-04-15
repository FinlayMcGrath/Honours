﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpearmanScript : UnitScript
{
	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_damage = 10;
		m_health = 60;
		m_cost = 20;
		m_speed = 10;
		m_trainingTime = 3.5;
		m_unitType = UnitType.infantry;
		m_bonusDamage = UnitType.cavalry;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
