using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private TurretProperties m_TurretProperties;

    private float m_RefireTimer;

    public bool CanFire => m_RefireTimer <= 0;

    private SpaceShip m_Ship;

    private void Start()
    {
        m_Ship = transform.root.GetComponent<SpaceShip>();
    }

    private void Update()
    {
        if (m_RefireTimer > 0)
        {
            m_RefireTimer -= Time.deltaTime;
        }
    }

    // Public API
    public void Fire()
    {
        if (m_TurretProperties == null) return;
        if (m_RefireTimer > 0) return;

        // Primary turrets use energy, secondary turrets use ammo
        if (m_Mode == TurretMode.Primary)
        {
            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false)
                return;
        }
        else if (m_Mode == TurretMode.Secondary)
        {
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false)
                return;
        }


        Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.transform.up = transform.up;

        projectile.SetParentShooter(m_Ship);

        m_RefireTimer = m_TurretProperties.RateOfFire;

        {
            // SFX
        }
    }

    public void AssignLoadout(TurretProperties props)
    {
        if (m_Mode != props.Mode) return;

        m_RefireTimer = 0;
        m_TurretProperties = props;
    }
}