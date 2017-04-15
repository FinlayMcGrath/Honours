using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WarriorTextScript : MonoBehaviour
{

	private float m_warriors;
	private Text m_warriorsText;
	private PlayerScript m_playerScript;

	// Use this for initialization
	void Start()
	{
		m_warriorsText = GetComponent<Text>();
		m_playerScript = GetComponentInParent<PlayerScript>();
	}

	// Update is called once per frame
	void Update()
	{
		m_warriors = m_playerScript.GetNumWarriors();
		m_warriorsText.text = "Warriors: " + m_warriors.ToString();
	}
}
