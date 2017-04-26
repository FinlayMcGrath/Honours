using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorkerTextScript : MonoBehaviour
{
    private float m_workers;
    private Text m_workersText;
    private PlayerScript m_playerScript;

    // Use this for initialization
    void Start()
    {
        m_workersText = GetComponent<Text>();
        m_playerScript = GetComponentInParent<PlayerScript>();
		transform.position += FindObjectOfType<Canvas>().transform.position;
        transform.SetParent(FindObjectOfType<Canvas>().transform);
	}

    // Update is called once per frame
    void Update()
    {
		if (m_playerScript && m_playerScript.gameObject.activeSelf)
		{
			m_workers = m_playerScript.GetNumWorkers();
			m_workersText.text = "Workers: " + m_workers.ToString();
		}
		else
		{
			Destroy(gameObject);
		}
    }
}
