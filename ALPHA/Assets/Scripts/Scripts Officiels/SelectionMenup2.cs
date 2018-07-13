using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenup2 : MonoBehaviour 
{
	[SerializeField]
	private GameObject LeftHulk;

	[SerializeField]
	private GameObject LeftParasite;
	
	[SerializeField]
	private GameObject LeftSkeleton;

	[SerializeField]
	private GameObject RightHulk;

	[SerializeField]
	private GameObject RightParasite;
	
	[SerializeField]
	private GameObject RightSkeleton;	
	
	private void Awake()
	{
		switch(GameManager.Instance.m_Player1)
		{
          case 1:
              LeftHulk.SetActive(true);
              break;
          case 2:
              LeftParasite.SetActive(true);
              break;
          case 3:
              LeftSkeleton.SetActive(true);
              break;
		}
	}

	public void SetActiveHulk()
	{
		RightHulk.SetActive(true);
		RightParasite.SetActive(false);
		RightSkeleton.SetActive(false);
		GameManager.Instance.SetPlayerCharacter(2, 1);
		StartCoroutine(Fight());
	}
	
	public void SetActiveParasite()
	{
		RightHulk.SetActive(false);
		RightParasite.SetActive(true);
		RightSkeleton.SetActive(false);
		GameManager.Instance.SetPlayerCharacter(2, 2);
		StartCoroutine(Fight());
	}

	public void SetActiveSkeleton()
	{
		RightHulk.SetActive(false);
		RightParasite.SetActive(false);
		RightSkeleton.SetActive(true);
		GameManager.Instance.SetPlayerCharacter(2, 3);
		StartCoroutine(Fight());
	}

	private IEnumerator Fight()
	{
		yield return new WaitForSeconds(1f);
		LevelManager.Instance.ChangeLevel("FightScene");
	}
}
