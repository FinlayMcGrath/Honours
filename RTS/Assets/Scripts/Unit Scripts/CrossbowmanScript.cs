using UnityEngine;
using System.Collections;

public class CrossbowmanScript : UnitScript
{
	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_health = 50;
		m_damage = 16;
		m_cost = 30;
		m_speed = 12;
		m_trainingTime = 3.5;
		m_unitType = UnitType.ranged;
		m_bonusDamage = UnitType.none;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}

