using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour 
{
	[SerializeField]
	private Animator m_PumpkinHulk;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.D))
		{
			m_PumpkinHulk.SetBool("Bloc", true);
		}
		else if(Input.GetKeyUp(KeyCode.D))
		{
			m_PumpkinHulk.SetBool("Bloc", false);
		}
		else if(Input.GetKeyDown(KeyCode.Q))
		{
			m_PumpkinHulk.SetTrigger("Jab");
		}
		else if(Input.GetKeyDown(KeyCode.W))
		{
			m_PumpkinHulk.SetTrigger("Straight");
		}
		else if(Input.GetKeyDown(KeyCode.E))
		{
			m_PumpkinHulk.SetTrigger("LeftKick");
		}
		else if(Input.GetKeyDown(KeyCode.R))
		{
			m_PumpkinHulk.SetTrigger("RightKick");
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			m_PumpkinHulk.SetTrigger("Range");
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			m_PumpkinHulk.SetTrigger("BlocBreaker");
		}

	}
}
