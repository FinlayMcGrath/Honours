using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArcherTextScript : MonoBehaviour
{

	private float m_archers;
	private Text m_archersText;
	private PlayerScript m_playerScript;

	// Use this for initialization
	void Start()
	{
		m_archersText = GetComponent<Text>();
		m_playerScript = GetComponentInParent<PlayerScript>();
	}

	// Update is called once per frame
	void Update()
	{
		m_archers = m_playerScript.GetNumArchers();
		m_archersText.text = "Archers: " + m_archers.ToString();
	}
}
