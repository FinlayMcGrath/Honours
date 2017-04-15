using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public GameObject m_map;
	float m_speed;
	Camera m_camera;
	// Use this for initialization
	void Start ()
	{
		m_speed = 10;
		m_camera = GetComponent<Camera>();
    }

	void Update()
	{
		//allows camera to be moved along the y axis when the mouse is close to the edge of the screen
		//stops moving at the edge of the map
		if (m_map != null)
		{
			if (m_camera.transform.position.y + m_camera.orthographicSize < m_map.GetComponent<SpriteRenderer>().bounds.size.y / 2)
			{
				if (Input.mousePosition.y > Screen.height - Screen.height / 8)
				{
					transform.Translate(transform.up * m_speed);
				}
			}
			if (m_camera.transform.position.y - m_camera.orthographicSize > -m_map.GetComponent<SpriteRenderer>().bounds.size.y / 2)
			{
				if (Input.mousePosition.y < Screen.height / 8)
				{
					transform.Translate(transform.up * -m_speed);
				}
			}
		}
    }
}
