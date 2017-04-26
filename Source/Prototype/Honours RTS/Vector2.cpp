#include "vector2.h"
#include <math.h>

//Grant Clark's Vector 2 Class from Games Education Framework

void Vector2::Normalise()
{
	float length = Length();

	x /= length;
	y /= length;
}

float Vector2::LengthSqr() const
{
	return (x*x + y*y);
}

float Vector2::Length() const
{
	return sqrtf(x*x + y*y);
}

Vector2 Vector2::Rotate(float angle)
{
	Vector2 result;

	result.x = x*cosf(angle) - y*sinf(angle);
	result.y = x*sinf(angle) + y*cosf(angle);

	return result;
}

float Vector2::DotProduct(const Vector2& _vec) const
{
	return x*_vec.x + y*_vec.y;
}

inline Vector2::Vector2()
{
}

inline Vector2::~Vector2()
{
}

inline Vector2::Vector2(const float new_x, const float new_y)
{
	x = new_x;
	y = new_y;
}

inline const Vector2 Vector2::operator-(const Vector2& vec) const
{
	Vector2 result;

	result.x = x-vec.x;
	result.y = y-vec.y;

	return result;
}

inline const Vector2 Vector2::operator+(const Vector2& vec) const
{
	Vector2 result;

	result.x = x+vec.x;
	result.y = y+vec.y;

	return result;
}

inline Vector2& Vector2::operator+=(const Vector2& vec)
{
	x += vec.x;
	y += vec.y;

	return *this;
}

inline Vector2& Vector2::operator-=(const Vector2& vec)
{
	x -= vec.x;
	y -= vec.y;

	return *this;
}

inline const Vector2 Vector2::operator*(const float scalar) const
{
	Vector2 result;

	result.x = x*scalar;
	result.y = y*scalar;

	return result;
}

inline const Vector2 Vector2::operator/(const float scalar) const
{
	Vector2 result;

	result.x = x/scalar;
	result.y = y/scalar;

	return result;
}

inline Vector2& Vector2::operator*=(const float scalar)
{
	x *= scalar;
	y *= scalar;

	return *this;
}

inline Vector2& Vector2::operator/=(const float scalar)
{
	x /= scalar;
	y /= scalar;

	return *this;
}