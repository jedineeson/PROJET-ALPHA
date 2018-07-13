using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerScreen : MonoBehaviour 
{
	[SerializeField]
	private GameObject m_Player1Text;
	[SerializeField]
	private GameObject m_Player2Text;
	[SerializeField]
	private GameObject m_Hulk;
	[SerializeField]
	private GameObject m_Parasite;
	[SerializeField]
	private GameObject m_Skeleton;
	
	private void Awake ()
	{
		m_Player1Text.SetActive(false);
		m_Player2Text.SetActive(false);			
        m_Hulk.SetActive(false);
        m_Parasite.SetActive(false);
    	m_Skeleton.SetActive(false);	
	}

	private void Start () 
	{
		if(GameManager.Instance.m_Winner == 1)
		{
			m_Player1Text.SetActive(true);
        	switch (GameManager.Instance.m_Player1)
        	{
            	case 1:
                	m_Hulk.SetActive(true);
                	break;
            	case 2:
                	m_Parasite.SetActive(true);
                	break;
            	case 3:
                	m_Skeleton.SetActive(true);
                break;
        	}
		}	
		else if(GameManager.Instance.m_Winner == 2)
		{
			m_Player2Text.SetActive(true);			
        	switch (GameManager.Instance.m_Player1)
        	{
            	case 1:
                	m_Hulk.SetActive(true);
                	break;
            	case 2:
                	m_Parasite.SetActive(true);
                	break;
            	case 3:
                	m_Skeleton.SetActive(true);
                break;
        	}
		}
	}
}
