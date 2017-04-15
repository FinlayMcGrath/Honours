#include "Player.h"

Player::Player()
{
	m_money = 0;
	for (int i = 0; i < 5; i++)
	{
		Worker worker;
		m_workers.push_back(worker);
	}
}

Player::~Player()
{

}

void Player::Update(float fps)
{
	m_hq.Update(fps);
	for (auto worker : m_workers)
	{
		worker.Update(fps);
	}

	for (auto warrior : m_warriors)
	{
		warrior.Update(fps);
	}

	if (m_workers.size() < 10)
	{
		Worker *worker = (Worker*)m_hq.GetTrainedUnit();
		if (worker)
		{
			m_workers.push_back(*worker);
		}
		m_money -= m_hq.BuildWorker(m_money);
	}
	else
	{
		Warrior *warrior = (Warrior*)m_hq.GetTrainedUnit();
		if (warrior)
		{
			m_warriors.push_back(*warrior);
		}
		m_money -= m_hq.BuildWarrior(m_money);
	}

	m_money += m_workers.size() / fps;
}


void Player::Render(sf::RenderWindow &window)
{
	m_hq.Render(window);
	for (auto worker : m_workers)
	{
		worker.Render(window);
	}
	for (auto warrior : m_workers)
	{
		warrior.Render(window);
	}
}
