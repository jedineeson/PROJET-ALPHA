using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public Animator m_Animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			m_Animator.Play("Jab");
		}
		else if(Input.GetKeyDown(KeyCode.W))
		{
			m_Animator.Play("Straight");
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			m_Animator.Play("LeftKick");
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			m_Animator.Play("RightKick");
		}
		else if(Input.GetKeyDown(KeyCode.E))
		{
			m_Animator.Play("Power");
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			m_Animator.Play("Block");
		}
	}
}
