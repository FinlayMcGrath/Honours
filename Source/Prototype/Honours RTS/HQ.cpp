#include "HQ.h"

HQ::HQ()
{
	m_busy = false;
	m_trainedUnit = false;
	m_trainingTime = 0;
	m_unitTraining = new Worker();
}

HQ::~HQ()
{

}

void HQ::Update(float fps)
{
	if (m_busy)
	{
		m_trainingTime += fps;
		if (m_trainingTime > m_unitTraining->GetTrainingTime())
		{
			m_trainingTime = 0;
			m_busy = false;
			m_trainedUnit = true;
		}
	}
}

//if HQ isn't busy, start building a worker and return the cost of the worker
int HQ::BuildWorker(float money)
{
	if (!m_busy && money > m_unitTraining->GetCost())
	{
		m_busy = true;
		m_unitTraining = new Worker();
		return m_unitTraining->GetCost();
	}
	return 0;
}

//if HQ isn't busy, start building a warrior and return the cost of the warrior
int HQ::BuildWarrior(float money)
{
	if (!m_busy && money > m_unitTraining->GetCost())
	{
		m_busy = true;
		m_unitTraining = new Warrior();
		return m_unitTraining->GetCost();
	}
	return 0;
}