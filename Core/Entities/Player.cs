using System;
using SpaceShooter;
using UnityEngine;

public class Player : SingletonBase<Player>
{
    public static SpaceShip SelectedSpaceShip;
    
    [SerializeField] private int m_NumLives;
    [SerializeField] private SpaceShip m_PlayerShipPrefab;

    [SerializeField] private CameraController m_CameraController;
    [SerializeField] private ShipInputController m_MovementController;
    [SerializeField] private StarfieldParallaxController m_ParallaxController;

    private SpaceShip m_Ship;
    public SpaceShip ActiveShip => m_Ship;

    private int m_Score;
    private int m_NumKills;

    public int Score => m_Score;
    public int NumKills => m_NumKills;
    public int NumLives => m_NumLives;

    public SpaceShip ShipPrefab
    {
        get
        {
            if (SelectedSpaceShip == null)
            {
                return m_PlayerShipPrefab;
            }
            else
            {
                return SelectedSpaceShip;
            }
        }
    }

    private void Start()
    {
        Respawn();
    }

    private void OnShipDeath()
    {
        m_NumLives--;

        if (m_NumLives > 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        var newPlayerShip = Instantiate(ShipPrefab);

        m_Ship = newPlayerShip.GetComponent<SpaceShip>();

        m_CameraController.SetTarget(m_Ship.transform);
        m_MovementController.SetTargetShip(m_Ship);

        m_Ship.EventOnDeath.AddListener(OnShipDeath);

        if (m_ParallaxController != null)
        {
            m_ParallaxController.target = m_Ship.transform;
        }
    }

    public void AddKill()
    {
        m_NumKills += 1;
    }

    public void AddScore(int num)
    {
        m_Score += num;
    }

}
