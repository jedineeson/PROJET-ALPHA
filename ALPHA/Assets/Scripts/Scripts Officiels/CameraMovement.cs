using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CenterPoint;

	private GameObject m_Player1;
    private GameObject m_Player2;

	private Vector3 m_Direction;
	private Vector3 m_PointVector;

	public Camera m_Cam;
	
	private void Start()
	{
		m_Player1 = SetFightScene.Instance.Player1;
		m_Player2 = SetFightScene.Instance.Player2;
	} 

	private void LateUpdate () 
	{
		float distance = Vector3.Distance(m_Player1.transform.position, m_Player2.transform.position);
		m_PointVector = (m_Player1.transform.position + m_Player2.transform.position) / 2;
		m_PointVector.y = m_Player2.transform.position.y;
		
		m_CenterPoint.transform.position = m_PointVector;

		m_CenterPoint.transform.right = m_Player2.transform.forward;			
	
		m_Cam.fieldOfView = distance;
	}
}
