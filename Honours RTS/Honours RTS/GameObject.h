#pragma once

#include <SFML/Graphics.hpp>
#include "Vector2.h"

class GameObject
{
public:
	GameObject();
	~GameObject();
	virtual void Update(float fps);
	void Render(sf::RenderWindow &window);
	void setPosition(Vector2 position) { m_position = position; };
	Vector2 getPosition() { return m_position; };
	void setHealth(float health) { m_health = health; };
	float getHealth() { return m_health; };
protected:
	sf::RectangleShape m_body;
	Vector2 m_position;
	float m_health;
};

