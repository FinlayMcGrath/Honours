using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>().m_map = gameObject;
    }
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
