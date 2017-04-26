#pragma once
#include "GameObject.h"
#include "Unit.h"

class Building : public GameObject
{
public:
	Building();
	~Building();
	void Update(float fps);
	Unit* GetTrainedUnit();
	bool GetBusy() { return m_busy; };
protected:
	Unit *m_unitTraining;
	bool m_busy, m_trainedUnit;
	float m_trainingTime;
};

