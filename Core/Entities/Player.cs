using SpaceShooter;
using UnityEngine;

public class Player : SingletonBase<Player>
{
    [SerializeField] private int m_NumLives;
    [SerializeField] private SpaceShip m_Ship;
    [SerializeField] private GameObject m_PlayerShipPrefab;
    public SpaceShip ActiveShip => m_Ship;

    [SerializeField] private CameraController m_CameraController;
    [SerializeField] private MovementController m_MovementController;
    [SerializeField] private StarfieldParallaxController m_ParallaxController;

    private void Start()
    {
        m_Ship.EventOnDeath.AddListener(OnShopDeath);
    }

    private void OnShopDeath()
    {
        m_NumLives--;

        if (m_NumLives > 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        var newPlayerShip = Instantiate(m_PlayerShipPrefab);
        m_Ship = newPlayerShip.GetComponent<SpaceShip>();

        m_CameraController.SetTarget(m_Ship.transform);
        m_MovementController.SetTargetShip(m_Ship);

        if (m_ParallaxController != null)
        {
            m_ParallaxController.target = m_Ship.transform;
        }
    }
}
