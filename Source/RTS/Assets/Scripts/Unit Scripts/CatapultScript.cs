using UnityEngine;
using System.Collections;

public class CatapultScript : UnitScript
{
	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_unitType = UnitType.siege;
		m_bonusDamage = UnitType.building;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
