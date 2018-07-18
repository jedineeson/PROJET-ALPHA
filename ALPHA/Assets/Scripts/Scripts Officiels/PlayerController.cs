using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<float, float, int> m_Action;
    private Vector3 m_rayPos;
    //Joueur 1/Joueur 2?
    [HideInInspector]
    public int m_PlayerID;
    [HideInInspector]
    public Rigidbody m_Player;
    //Mon Ennemie
    [HideInInspector]
    public Rigidbody m_Ennemy;
    //Bool pour activer et désactiver les contrôles
    [HideInInspector]
    public bool m_CanControl = false;
    [HideInInspector]
    public bool m_IsBlockin = false;
    //Data paramétrable
    [SerializeField]
    private PlayerData m_Data;
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private BoxCollider m_LeftHandCollider;
    [SerializeField]
    private BoxCollider m_RightHandCollider;
    [SerializeField]
    private BoxCollider m_LeftKickCollider;
    [SerializeField]
    private BoxCollider m_RightKickCollider;
    //Je dash combien de temps
    private float m_IsDashingTimer;
    //Vitesse maximum du déplacement du joueur
    private float m_ZMoveSpeed;
    private float m_ZDashSpeed;
    private float m_XMoveSpeed;
    private float m_XDashSpeed;
    //Coût en stamina/vie d'un Dash
    private float m_DashCost;
    //Stamina/Vie
    private float m_Life;
    //Stamina/Vie MAX
    private float m_LifeMax;
    private float m_DoubleTapDelay;
    //Double Tap Count
    private int m_TapCountX = 0;
    private int m_TapCountZ = 0;
    //Timer du double tap
    private float m_CurrentDoubleTapTime = 0f;
    private float m_Distance;
    private float m_RelativeZDashSpeed;
    //Vitesse de déplacement actuel du joueur en X
    private float m_ActualSpeedX = 0f;
    //Vitesse de déplacement actuel du joueur en Z
    private float m_ActualSpeedZ = 0f;
    //Quantité de stamina qu'il reste à récupérer
    //private float m_Recovery = 0;
    //Valeur de l'Input en X
    private float m_InputX = 0f;
    //Valeur de l'Input en Z
    private float m_InputZ = 0f;
    //Est-ce que je suis en train de dasher?
    private bool m_IsDashingHorizontal = false;
    //Est-ce que je suis en train de dasher?
    private bool m_IsDashingVertical = false;
    private float m_JabCost;
    private float m_StraightCost;
    private float m_LeftKickCost;
    private float m_RightKickCost;

    //vecteur pour direction de mon personnage
    private Vector3 m_MoveDir = new Vector3();
    //vecteur pour position de mon ennemie
    private Vector3 m_EnnemyPosition = new Vector3();

    public float Life
    {
        get { return m_Life; }
    }

    public float LifeMax
    {
        get { return m_LifeMax; }
    }

    void Start()
    {
        m_LeftHandCollider.enabled = false;
        m_RightHandCollider.enabled = false;
        m_LeftKickCollider.enabled = false;
        m_RightKickCollider.enabled = false;
        m_JabCost = m_Data.JabCost;
        m_StraightCost = m_Data.StraightCost;
        m_LeftKickCost = m_Data.LeftKickCost;
        m_RightKickCost = m_Data.RightKickCost;
        m_IsDashingTimer = m_Data.IsDashingTimer;
        m_ZMoveSpeed = m_Data.ZMoveSpeed;
        m_ZDashSpeed = m_Data.ZDashSpeed;
        m_XMoveSpeed = m_Data.XMoveSpeed;
        m_XDashSpeed = m_Data.XDashSpeed;
        m_DashCost = m_Data.DashCost;
        m_Life = m_Data.InitLife;
        m_LifeMax = m_Life;
        m_DoubleTapDelay = m_Data.DoubleTapDelay;

        m_ActualSpeedX = m_XMoveSpeed;
        m_ActualSpeedZ = m_ZMoveSpeed;

    }

    void Update()
    {
        if (m_Ennemy != null)
        {
            //vecteur pour position de mon ennemie = position du transform de mon ennemie
            m_EnnemyPosition = m_Ennemy.transform.position;
            //Mon Player regarde toujours en direction de l'ennemi
            m_EnnemyPosition.y = transform.position.y;
            //Si je frappe Vertical je ne suis plus mon adversaire des yeux, il peut donc dasher 
            //transform.LookAt(m_EnnemyPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.LookRotation(m_EnnemyPosition - transform.position)), Time.deltaTime * 2);
        }

        /*if (m_IsBlockin)
        {
            m_ActualSpeedX = m_XMoveSpeed * 0.5f;
            m_ActualSpeedZ = m_ZMoveSpeed * 0.5f;
            m_TapCountX = 0;
            m_TapCountZ = 0;
        }
        else
        {*/
        m_ActualSpeedX = m_XMoveSpeed;
        m_ActualSpeedZ = m_ZMoveSpeed;
        //}

        if (m_Life < m_LifeMax)
        {
            if (m_IsBlockin)
            {
                m_Life += Time.deltaTime;
            }
            else
            {
                m_Life += Time.deltaTime * 4;
            }
            if (m_Action != null)
            {
                m_Action(m_Life, m_LifeMax, m_PlayerID);
            }
        }
        else if (m_Life > m_LifeMax)
        {
            m_Life = m_LifeMax;

            if (m_Action != null)
            {
                m_Action(m_Life, m_LifeMax, m_PlayerID);
            }
        }

        if (m_CanControl)
        {
            if (Input.GetButton("Bloc_p" + m_PlayerID))
            {
                m_Animator.SetBool("Bloc", true);
                m_IsBlockin = true;
            }
            else if (Input.GetButtonUp("Bloc_p" + m_PlayerID))
            {
                m_Animator.SetBool("Bloc", false);
                m_IsBlockin = false;
            }
            else if (Input.GetButtonDown("Jab_p" + m_PlayerID) && m_Life > m_JabCost + 1)
            {
                m_Life -= m_JabCost;
                m_Animator.SetTrigger("Jab");
                m_LeftHandCollider.enabled = true;
                m_RightHandCollider.enabled = false;
                m_LeftKickCollider.enabled = false;
                m_RightKickCollider.enabled = false;
            }
            else if (Input.GetButtonDown("Straight_p" + m_PlayerID) && m_Life > m_StraightCost + 1)
            {
                m_Life -= m_StraightCost;
                m_Animator.SetTrigger("Straight");
                m_LeftHandCollider.enabled = false;
                m_RightHandCollider.enabled = true;
                m_LeftKickCollider.enabled = false;
                m_RightKickCollider.enabled = false;
            }
            else if (Input.GetButtonDown("LeftKick_p" + m_PlayerID) && m_Life > m_LeftKickCost + 1)
            {
                m_Life -= m_LeftKickCost;
                m_Animator.SetTrigger("LeftKick");
                m_LeftHandCollider.enabled = false;
                m_RightHandCollider.enabled = false;
                m_LeftKickCollider.enabled = true;
                m_RightKickCollider.enabled = false;
            }
            else if (Input.GetButtonDown("RightKick_p" + m_PlayerID))
            {
                m_Life -= m_RightKickCost;
                m_Animator.SetTrigger("RightKick");
                m_LeftHandCollider.enabled = false;
                m_RightHandCollider.enabled = false;
                m_LeftKickCollider.enabled = false;
                m_RightKickCollider.enabled = true;
            }
            else if (Input.GetButtonDown("BlocBreaker_p" + m_PlayerID) && m_Life > m_RightKickCost + 1)
            {
                m_Animator.Play("BlocBreaker");
            }
            else if (Input.GetButtonDown("Range_p" + m_PlayerID))
            {
                m_Animator.Play("Range");
            }

            if (m_IsDashingHorizontal || m_IsDashingVertical)
            {
                return;
            }

            CheckInputAxis();
            CheckDoubletapZ();
            m_rayPos.y = transform.position.y + 1f;
            m_rayPos.x = transform.position.x;
            m_rayPos.z = transform.position.z;

            if (Physics.Raycast(m_rayPos, transform.forward, 0.8f))
            {
                m_InputX = Mathf.Clamp(m_InputX, -1f, 0f);
                Debug.DrawRay(m_rayPos, transform.forward, Color.green);
            }
            else
            {
                CheckDoubletapX();
                Debug.DrawRay(m_rayPos, transform.forward, Color.red);
            }
        }

        if (m_Life <= 0)
        {
            if (m_PlayerID == 1)
            {
                GameManager.Instance.SetWinner(2);
            }
            else if (m_PlayerID == 2)
            {
                GameManager.Instance.SetWinner(1);
            }

            LevelManager.Instance.ChangeLevel("WinnerScreen");
        }
    }

    private void FixedUpdate()
    {
        m_MoveDir = (m_InputX * transform.forward);
        m_MoveDir *= m_ActualSpeedX;
        if (m_Player != null)
        {
            m_MoveDir.y = m_Player.velocity.y;
            m_Player.velocity = m_MoveDir;
            //tourner en un cercle parfait autours de l'adversaire
            transform.RotateAround(m_EnnemyPosition, (m_InputZ * Vector3.up), m_ActualSpeedZ * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Hit"))
        {
            if (m_IsBlockin)
            {
                Damage(5, 1);
            }
            else
            {
                Damage(10, 5);
            }
            Debug.Log("vie " + m_PlayerID + " : " + m_Life);
        }
    }

    //On vérifie l'Input en X, on donne sa valeur à m_InputX et on retourne vrai si il y a un input;
    private void CheckInputAxis()
    {
        //GetAxisRaw retourne toujours un chiffre précis : -1, 0 ou 1
        m_InputX = Input.GetAxisRaw("Horizontal_p" + m_PlayerID);
        m_InputZ = Input.GetAxisRaw("Vertical_p" + m_PlayerID);
        m_Animator.SetInteger("SideWalk", (int)m_InputZ);
        m_Animator.SetInteger("FrontWalk", (int)m_InputX);
    }

    private void CheckDoubletapX()
    {
        //Si j'ai double-tap et que j'ai l'énergie nécessaire je dash
        if (m_TapCountX >= 2 && m_Life >= m_DashCost + 1)
        {
            StopCoroutine(DashingHorizontal());
            StartCoroutine(DashingHorizontal());
            m_TapCountX = 0;
            m_CurrentDoubleTapTime = 0f;
        }
        //Si c'est mon premier tap ajoute 1 à TapCount
        if (Input.GetButtonDown("Horizontal_p" + m_PlayerID))
        {
            if (m_TapCountX == 0)
            {
                m_CurrentDoubleTapTime = 0f;
            }
            m_TapCountX++;
        }
        //Vérifie le délais en 2 tap, remet TapCount à 0 si le délais est dépassé.
        m_CurrentDoubleTapTime += Time.deltaTime;
        if (m_CurrentDoubleTapTime >= m_DoubleTapDelay)
        {
            m_TapCountX = 0;
            m_CurrentDoubleTapTime = 0f;
        }
    }

    private void CheckDoubletapZ()
    {
        //Si j'ai double-tap et que j'ai l'énergie nécessaire je dash
        if (m_TapCountZ >= 2 && m_Life >= m_DashCost + 1)
        {
            StopCoroutine(DashingVertical());
            StartCoroutine(DashingVertical());
            m_TapCountZ = 0;
            m_CurrentDoubleTapTime = 0f;
        }
        //Si c'est mon premier tap ajoute 1 à TapCount
        if (Input.GetButtonDown("Vertical_p" + m_PlayerID))
        {
            if (m_TapCountZ == 0)
            {
                m_CurrentDoubleTapTime = 0f;
            }
            m_TapCountZ++;
        }
        //Vérifie le délais en 2 tap, remet TapCount à 0 si le délais est dépassé.
        m_CurrentDoubleTapTime += Time.deltaTime;
        if (m_CurrentDoubleTapTime >= m_DoubleTapDelay)
        {
            m_TapCountZ = 0;
            m_CurrentDoubleTapTime = 0f;
        }
    }

    private void SetColliderFalse()
    {
        m_LeftHandCollider.enabled = false;
        m_RightHandCollider.enabled = false;
        m_LeftKickCollider.enabled = false;
        m_RightKickCollider.enabled = false;
    }

    private void Damage(float life, float lifeMax)
    {
        m_Life -= life;
        m_LifeMax -= lifeMax;
    }

    //Déroulement du dash horizontal
    private IEnumerator DashingHorizontal()
    {
        //Distance entre les 2 joueurs
        m_Distance = Vector3.Distance(m_EnnemyPosition, transform.position);
        //Réduit la distance du dash relativement à la distance entre les 2 joueurs
        m_RelativeZDashSpeed = m_ZDashSpeed / m_Distance;
        m_ActualSpeedX = m_XDashSpeed;
        Damage(5, 1);
        if (m_InputX == 1)
        {
            //m_Animator.Play("StepFront");
        }
        else if (m_InputX == -1)
        {
            //m_Animator.Play("StepBack");
        }
        m_IsDashingHorizontal = true;
        yield return new WaitForSeconds(m_IsDashingTimer);
        m_IsDashingHorizontal = false;
        m_ActualSpeedX = m_XMoveSpeed;
    }
    //Déroulement du dash vertical
    private IEnumerator DashingVertical()
    {
        Debug.Log("Dash");
        //Distance entre les 2 joueurs
        m_Distance = Vector3.Distance(m_EnnemyPosition, transform.position);
        //Réduit la distance du dash relativement à la distance entre les 2 joueurs
        m_RelativeZDashSpeed = m_ZDashSpeed / m_Distance;
        m_ActualSpeedZ = m_RelativeZDashSpeed;
        Damage(5, 1);
        m_IsDashingHorizontal = true;
        yield return new WaitForSeconds(m_IsDashingTimer);
        Debug.Log("DashOver");
        m_IsDashingHorizontal = false;
        m_ActualSpeedZ = m_ZMoveSpeed;
    }
}
