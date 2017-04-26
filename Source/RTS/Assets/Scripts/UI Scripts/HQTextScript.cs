using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HQTextScript : MonoBehaviour
{
    private int m_hqHealth;
    private Text m_hqHealthText;
    private PlayerScript m_playerScript;

    // Use this for initialization
    void Start()
    {
        m_hqHealthText = GetComponent<Text>();
        m_playerScript = GetComponentInParent<PlayerScript>();
		transform.position += FindObjectOfType<Canvas>().transform.position;
		transform.SetParent(FindObjectOfType<Canvas>().transform);
	}

    // Update is called once per frame
    void Update()
    {
		if (m_playerScript && m_playerScript.gameObject.activeSelf)
		{
			m_hqHealth = (int)m_playerScript.m_hq.GetComponent<HQScript>().m_health;
			m_hqHealthText.text = "HQ Health: " + m_hqHealth.ToString();
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
