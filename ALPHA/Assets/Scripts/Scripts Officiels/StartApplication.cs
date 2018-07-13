using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartApplication : MonoBehaviour 
{
	private void Start () 
	{
		LevelManager.Instance.ChangeLevel("Menu p1");
	}
}
