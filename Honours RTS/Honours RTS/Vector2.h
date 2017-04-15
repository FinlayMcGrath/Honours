#pragma once

//Grant Clark's Vector 2 Class from Games Education Framework

class Vector2
{
public:

	Vector2();
	~Vector2();
	Vector2(const float x, const float y);

	const Vector2 operator - (const Vector2& vec) const;
	const Vector2 operator + (const Vector2& vec) const;
	Vector2& operator -= (const Vector2& vec);
	Vector2& operator += (const Vector2& _vec);
	const Vector2 operator * (const float scalar) const;
	const Vector2 operator / (const float scalar) const;
	Vector2& operator *= (const float scalar);
	Vector2& operator /= (const float scalar);

	void Normalise();
	float LengthSqr() const;
	float Length() const;
	Vector2 Rotate(float angle);
	float DotProduct(const Vector2& _vec) const;


	float x;
	float y;
};

