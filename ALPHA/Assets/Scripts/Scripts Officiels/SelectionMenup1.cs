using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenup1 : MonoBehaviour 
{
	[SerializeField]
	private GameObject Hulk;

	[SerializeField]
	private GameObject Parasite;
	
	[SerializeField]
	private GameObject Skeleton;

	public void SetActiveHulk()
	{
		Hulk.SetActive(true);
		Parasite.SetActive(false);
		Skeleton.SetActive(false);
		GameManager.Instance.SetPlayerCharacter(1, 1);
		LevelManager.Instance.ChangeLevel("Menu p2");
	}
	
	public void SetActiveParasite()
	{
		Hulk.SetActive(false);
		Parasite.SetActive(true);
		Skeleton.SetActive(false);
		GameManager.Instance.SetPlayerCharacter(1, 2);
		LevelManager.Instance.ChangeLevel("Menu p2");
	}

	public void SetActiveSkeleton()
	{
		Hulk.SetActive(false);
		Parasite.SetActive(false);
		Skeleton.SetActive(true);
		GameManager.Instance.SetPlayerCharacter(1, 3);
		LevelManager.Instance.ChangeLevel("Menu p2");
	}
}
