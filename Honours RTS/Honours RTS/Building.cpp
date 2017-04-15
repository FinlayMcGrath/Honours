#include "Building.h"

Building::Building()
{

}

Building::~Building()
{

}

void Building::Update(float fps)
{

}

Unit* Building::GetTrainedUnit()
{
	if (m_trainedUnit == true)
	{
		m_trainedUnit = false;
		return m_unitTraining;
	}
	return NULL;
}
