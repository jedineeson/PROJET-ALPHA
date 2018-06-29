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
			Debug.Log("Je suis le premier, je reste.");
			m_Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void SetPlayerCharacter(int ID, int choice)
	{
        if(ID == 1)
		{
        	m_Player1 = choice;
			Debug.Log("Player1" + choice);
		}

		if(ID == 2)
		{
        	m_Player2 = choice;
			Debug.Log("Player2" + choice);
		}
	}
}

