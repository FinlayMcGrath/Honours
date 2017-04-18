using UnityEngine;
using System.Collections;

public class CrossbowmanScript : UnitScript
{
	// Use this for initialization
	public override void Start()
	{
		base.Start();
		m_unitType = UnitType.ranged;
		m_bonusDamage = UnitType.none;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}

