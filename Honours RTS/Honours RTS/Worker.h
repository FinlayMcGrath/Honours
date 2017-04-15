#pragma once
#include "Unit.h"
class Worker :
	public Unit
{
public:
	Worker();
	~Worker();
	void Update(float fps);
};

