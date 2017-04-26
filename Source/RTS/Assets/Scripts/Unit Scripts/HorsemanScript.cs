using UnityEngine;
using System.Collections;

public class HorsemanScript : UnitScript
{

	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_unitType = UnitType.cavalry;
		m_bonusDamage = UnitType.ranged;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
