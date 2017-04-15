#pragma once

#include <SFML/Graphics.hpp>
#include <vector>
#include "HQ.h"
#include "Worker.h"
#include "Warrior.h"

class Player
{
public:
	Player();
	~Player();
	void Update(float fps);
	void Render(sf::RenderWindow &window);
	int GetMoney() { return m_money; };
	int GetNumWorkers() { return m_workers.size(); };
	int GetNumWarriors() { return m_warriors.size(); };
private:
	float m_money;
	HQ m_hq;
	std::vector<Worker> m_workers;
	std::vector<Warrior> m_warriors;
};

