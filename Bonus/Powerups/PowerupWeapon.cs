using UnityEngine;

/// <summary>
/// A PowerUp that grants the player a specific weapon and optionally adds ammo for it.
/// Inherits from the base PowerUp class.
/// </summary>
public class Powerup_weapon : PowerUp
{
    [SerializeField] private TurretProperties m_Properties;
    [SerializeField] private int m_AmmoToAdd = 10;

    protected override void OnPickedUp(SpaceShip ship)
    {
        ship.AssignWeapon(m_Properties);
        ship.AddAmmo(m_AmmoToAdd);
    }
}
