#pragma once
#include "Unit.h"
class Warrior :
	public Unit
{
public:
	Warrior();
	~Warrior();
	void Update(float fps);
};

