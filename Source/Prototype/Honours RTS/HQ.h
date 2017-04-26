#pragma once
#include "Building.h"
#include "Warrior.h"
#include "Worker.h"

class HQ :
	public Building
{
public:
	HQ();
	~HQ();
	int BuildWorker(float money);
	int BuildWarrior(float money);
	void Update(float fps);
private:

};

