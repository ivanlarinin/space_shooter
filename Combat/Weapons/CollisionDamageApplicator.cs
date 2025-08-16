using UnityEngine;

/// <summary>
/// Applies damage to this object when it collides with another object.
/// Damage is calculated as a constant value plus a multiplier based on collision speed.
/// </summary>
public class CollisionDamageApplicator : MonoBehaviour
{
    public static string IgnoreTag = "WorldBoundary";

    [SerializeField] private float m_VelocityDamageModifier;
    [SerializeField] private float m_DamageConstant;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == IgnoreTag)
        {
            return;
        }

        var destructible = transform.root.GetComponent<Destructable>();

        if (destructible != null)
        {
            destructible.ApplyDamage((int)m_DamageConstant +
                                    (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
        }
    }
}