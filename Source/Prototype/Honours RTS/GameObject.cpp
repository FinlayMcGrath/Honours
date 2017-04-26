#include "GameObject.h"



GameObject::GameObject()
{
	m_health = 0.0f;
}

GameObject::~GameObject()
{

}

void GameObject::Update(float fps)
{

}

void GameObject::Render(sf::RenderWindow &window)
{
	window.draw(m_body);
}
