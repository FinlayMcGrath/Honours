using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
	private float m_money;
	private Text m_moneyText;
	private PlayerScript m_playerScript;

	// Use this for initialization
	void Start()
	{
		m_moneyText = GetComponent<Text>();
		m_playerScript = GetComponentInParent<PlayerScript>();
		transform.position += FindObjectOfType<Canvas>().transform.position;
		transform.SetParent(FindObjectOfType<Canvas>().transform);
	}

	// Update is called once per frame
	void Update()
	{
		if (m_playerScript && m_playerScript.gameObject.activeSelf)
		{
			m_money = m_playerScript.GetMoney();
			m_moneyText.text = "Money: " + m_money.ToString();
		}
		else 
		{
			Destroy(gameObject);
		}
	}
}
