using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetFightScene : MonoBehaviour
{
    private static SetFightScene m_Instance;
	public static SetFightScene Instance
	{		
		get { return m_Instance; }
	}

    private GameObject m_Player1;
    private GameObject m_Player2;
    public GameObject Player1
    {
        get { return m_Player1; }
    }
    public GameObject Player2
    {
        get { return m_Player2; }
    }

    [SerializeField]
    private GameObject m_P1StartPos;
    [SerializeField]
    private GameObject m_P2StartPos;
    [SerializeField]
    private GameObject m_HulkPrefab;
    [SerializeField]
    private GameObject m_ParasitePrefab;
    [SerializeField]
    private GameObject m_SkeletonPrefab;
    [SerializeField]
    private TextMeshProUGUI m_TextMesh;

    private GameObject m_TextMeshGameObject;

    private void Awake()
    {
        m_TextMeshGameObject = m_TextMesh.gameObject;
        m_TextMeshGameObject.SetActive(false);

        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }

        switch (GameManager.Instance.m_Player1)
        {
            case 1:
                m_Player1 = Instantiate(m_HulkPrefab, m_P1StartPos.transform.position, m_P1StartPos.transform.rotation);
                break;
            case 2:
                m_Player1 = Instantiate(m_ParasitePrefab, m_P1StartPos.transform.position, m_P1StartPos.transform.rotation);
                break;
            case 3:
                m_Player1 = Instantiate(m_SkeletonPrefab, m_P1StartPos.transform.position, m_P1StartPos.transform.rotation);
                break;
        }

        switch (GameManager.Instance.m_Player2)
        {
            case 1:
                m_Player2 = Instantiate(m_HulkPrefab, m_P2StartPos.transform.position, m_P2StartPos.transform.rotation);
                break;
            case 2:
                m_Player2 = Instantiate(m_ParasitePrefab, m_P2StartPos.transform.position, m_P2StartPos.transform.rotation);
                break;
            case 3:
                m_Player2 = Instantiate(m_SkeletonPrefab, m_P2StartPos.transform.position, m_P2StartPos.transform.rotation);
                break;
        }

        m_Player1.GetComponent<PlayerController>().m_PlayerID = 1;
        m_Player1.GetComponent<PlayerController>().m_Player = m_Player1.GetComponent<Rigidbody>();
        m_Player1.GetComponent<PlayerController>().m_Ennemy = m_Player2.GetComponent<Rigidbody>();

        m_Player2.GetComponent<PlayerController>().m_PlayerID = 2;
        m_Player2.GetComponent<PlayerController>().m_Player = m_Player2.GetComponent<Rigidbody>();
        m_Player2.GetComponent<PlayerController>().m_Ennemy = m_Player1.GetComponent<Rigidbody>();

    }

    private void Start()
    {
        StartCoroutine(FightCount());
    }

    private IEnumerator FightCount()
    {
        yield return new WaitForSeconds(0.5f);
        m_TextMesh.text = "3";
        m_TextMeshGameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        m_TextMeshGameObject.SetActive(false);        
        yield return new WaitForSeconds(0.5f);        
        m_TextMesh.text = "2";
        m_TextMeshGameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        m_TextMeshGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        m_TextMesh.text = "1";
        m_TextMeshGameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        m_TextMeshGameObject.SetActive(false);      
        yield return new WaitForSeconds(0.5f);
        m_TextMesh.text = "FIGHT!";
        m_Player1.GetComponent<PlayerController>().m_CanControl = true;
        m_Player2.GetComponent<PlayerController>().m_CanControl = true;        
        m_TextMeshGameObject.SetActive(true);        
        yield return new WaitForSeconds(0.5f);
        m_TextMeshGameObject.SetActive(false);          
    }
}


