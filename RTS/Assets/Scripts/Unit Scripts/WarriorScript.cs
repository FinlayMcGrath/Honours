﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarriorScript : UnitScript
{
	// Use this for initialization
	public override void Start ()
	{
		base.Start();
		m_unitType = UnitType.infantry;
		m_bonusDamage = UnitType.none;
    }
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();
    }
}
