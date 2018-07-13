using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class TargetGroupLinker : MonoBehaviour 
{
	public GameObject LookAt1;
	public GameObject LookAt2;

	private void Start () 
	{
		 LookAt1.transform.SetParent(SetFightScene.Instance.Player2.gameObject.transform);
		 LookAt2.transform.SetParent(SetFightScene.Instance.Player1.gameObject.transform);		 
	}
}
