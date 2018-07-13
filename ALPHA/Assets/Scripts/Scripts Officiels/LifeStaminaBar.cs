using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeStaminaBar : MonoBehaviour
{
    [SerializeField]
    private PlayerData m_Data; 
    [SerializeField]
    private Image m_LifeBar1;
    [SerializeField]
    private Image m_LifeBar2;
    [SerializeField]
	private Image m_LifeMaxBar1;
    [SerializeField]
    private Image m_LifeMaxBar2;    

	private void Awake()
	{
		SetFightScene.Instance.Player1.GetComponent<PlayerController>().m_Action += OnUpdateLifeBar;
		SetFightScene.Instance.Player2.GetComponent<PlayerController>().m_Action += OnUpdateLifeBar;
	}

	private void OnUpdateLifeBar (float life, float maxLife, int id)
	{
		if(id == 1)
		{
			m_LifeBar1.fillAmount = life / m_Data.InitLife;
			m_LifeMaxBar1.fillAmount = maxLife / m_Data.InitLife;
		}
		else if(id == 2)
		{
			m_LifeBar2.fillAmount = life / m_Data.InitLife;
			m_LifeMaxBar2.fillAmount = maxLife / m_Data.InitLife;
		}
	}
}


