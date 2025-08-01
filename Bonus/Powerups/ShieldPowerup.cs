using UnityEngine;

/// <summary>
/// Powerup that restores shield health when collected
/// </summary>
public class ShieldPowerup : PowerUp
{
    [Header("Shield Restoration")]
    [SerializeField] private float m_ShieldRestoreAmount = 50f;
    [SerializeField] private GameObject m_CollectEffect;
    
    protected override void OnPickedUp(SpaceShip ship)
    {
        ShieldSystem shieldSystem = ship.GetComponent<ShieldSystem>();
        
        if (shieldSystem != null)
        {
            shieldSystem.RestoreShield(m_ShieldRestoreAmount);
            
            // Optional: Play collection effect
            if (m_CollectEffect != null)
            {
                Instantiate(m_CollectEffect, ship.transform.position, Quaternion.identity);
            }
        }
    }
} 