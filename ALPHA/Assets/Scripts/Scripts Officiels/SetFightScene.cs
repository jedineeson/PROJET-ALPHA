using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFightScene : MonoBehaviour 
{

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
	
	private void Awake()
	{
		switch(GameManager.Instance.m_Player1)
		{
          case 1:
              Instantiate(m_HulkPrefab, m_P1StartPos.transform.position, m_P1StartPos.transform.rotation);
              break;
          case 2:
              Instantiate(m_ParasitePrefab, m_P1StartPos.transform.position, m_P1StartPos.transform.rotation);
              break;
          case 3:
              Instantiate(m_SkeletonPrefab, m_P1StartPos.transform.position, m_P1StartPos.transform.rotation);
              break;
		}

		switch(GameManager.Instance.m_Player2)
		{
          case 1:
              Instantiate(m_HulkPrefab, m_P2StartPos.transform.position, m_P2StartPos.transform.rotation);
              break;
          case 2:
              Instantiate(m_ParasitePrefab, m_P2StartPos.transform.position, m_P2StartPos.transform.rotation);
              break;
          case 3:
              Instantiate(m_SkeletonPrefab, m_P2StartPos.transform.position, m_P2StartPos.transform.rotation);
              break;
		}
	}
}
