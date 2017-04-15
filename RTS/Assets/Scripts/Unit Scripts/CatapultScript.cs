using UnityEngine;
using System.Collections;

public class CatapultScript : UnitScript
{
	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_health = 40;
		m_damage = 30;
		m_cost = 100;
		m_speed = 5;
		m_trainingTime = 7.5;
		m_unitType = UnitType.siege;
		m_bonusDamage = UnitType.building;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
