#include "Application.h"

Application::Application()
{
	m_window.create(sf::VideoMode(1280, 720), "SFML works!");
	shape.setFillColor(sf::Color::Green);
	
	m_font.loadFromFile("resources/sansation.ttf");

	m_playerMoney.setFont(m_font);
	m_playerMoney.setPosition(100, 60);
	m_playerMoney.setString("Money: " + 0);
	m_playerMoney.setCharacterSize(30);
	m_playerMoney.setFillColor(sf::Color(255, 255, 255, 255));

	m_workerCount.setFont(m_font);
	m_workerCount.setPosition(100, 100);
	m_workerCount.setString("Workers: " + 0);
	m_workerCount.setCharacterSize(30);
	m_workerCount.setFillColor(sf::Color(255, 255, 255, 255));

	m_warriorCount.setFont(m_font);
	m_warriorCount.setPosition(100, 140);
	m_warriorCount.setString("Warriors: " + 0);
	m_warriorCount.setCharacterSize(30);
	m_warriorCount.setFillColor(sf::Color(255, 255, 255, 255));

}

Application::~Application()
{

}

bool Application::Update()
{
	float fps, frameTime;

	frameTime = m_clock.getElapsedTime().asSeconds();
	fps = 1.0f / frameTime;

	m_clock.restart();

	sf::Event event;
	if (m_window.isOpen())
	{
		while (m_window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
			{
				m_window.close();
				return false;
			}
		}
	}

	m_player.Update(fps);
	m_playerMoney.setString("Money: " + std::to_string(m_player.GetMoney()));
	m_workerCount.setString("Workers: " + std::to_string(m_player.GetNumWorkers()));
	m_warriorCount.setString("Warriors: " + std::to_string(m_player.GetNumWarriors()));
	return true;
}



void Application::Render()
{
	m_window.clear();
	m_player.Render(m_window);
	m_window.draw(m_playerMoney);
	m_window.draw(m_workerCount);
	m_window.draw(m_warriorCount);
	m_window.display();
}

bool Application::Run()
{
	bool running = true;
	while (running)
	{
		running = Update();
		Render();
	}
	return running;
}
