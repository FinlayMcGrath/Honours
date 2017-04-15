using UnityEngine;
using System.Collections;

public class ArcherScript : UnitScript
{
	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_health = 20;
		m_damage = 20;
		m_cost = 20;
		m_speed = 15;
		m_trainingTime = 2.5;
		m_unitType = UnitType.infantry;
		m_bonusDamage = UnitType.none;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
