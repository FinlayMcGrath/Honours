#pragma once
#include <SFML/Graphics.hpp>
#include <string>
#include "Player.h"

class Application
{
public:
	Application();
	~Application();
	bool Run();
private:
	bool Update();
	void Render();

	sf::RenderWindow m_window;
	Player m_player;
	sf::CircleShape shape;
	sf::Clock m_clock;
	sf::Text m_workerCount;
	sf::Text m_warriorCount;
	sf::Text m_playerMoney;
	sf::Font m_font;
};

