using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	/*[SerializeField]
	private GameObject m_Hulk;
	[SerializeField]	
	private GameObject m_Parasite;
	[SerializeField]
	private GameObject m_Skeleton;*/

	public int m_Player1;
	public int m_Player2;
	public int m_Winner;
	
	private static GameManager m_Instance;
	//variable de moi même(on peux y accéder de partout avec LevelManager.Instance)
	public static GameManager Instance
	{
		//on veux jamais le re-set		
		get { return m_Instance; }
	}

	private void Awake()
	{

		if(m_Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			m_Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void SetPlayerCharacter(int ID, int choice)
	{
        if(ID == 1)
		{
        	m_Player1 = choice;
		}

		if(ID == 2)
		{
        	m_Player2 = choice;
		}
	}

	public void SetWinner(int ID)
	{
		m_Winner = ID;
	}
}

