using UnityEngine;

/// <summary>
/// A specific type of PowerUp that modifies the player's ammo or energy when picked up.
/// Inherits from the base PowerUp class.
/// </summary>
public class PowerupStats : PowerUp
{
    public enum EffectType
    {
        AddAmmo,
        AddEnergy
    }

    [SerializeField] private EffectType m_EffectType;
    [SerializeField] private float m_Value;

    protected override void OnPickedUp(SpaceShip ship)
    {
        if (m_EffectType == EffectType.AddEnergy)
        {
            ship.AddEnergy((int)m_Value);
        }

        if (m_EffectType == EffectType.AddAmmo)
        {
            ship.AddAmmo((int)m_Value);
        }
    }
}
