#pragma once
#include "GameObject.h"
class Unit : public GameObject
{
public:
	Unit();
	~Unit();
	int GetTrainingTime() { return m_trainingTime; };
	int GetCost() { return m_cost; };
protected:
	Vector2 m_targetLocation;
	int m_trainingTime, m_cost;
};

