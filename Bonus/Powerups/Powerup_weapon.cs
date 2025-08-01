using UnityEngine;

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
