using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Уничтожаемый объект на сцене. То что может иметь хит поинты.
/// </summary>
public class Destructible : Entity
{
    #region Properties

    /// <summary>
    /// Объект игнорирует повреждения.
    /// </summary>
    [SerializeField] private bool m_Indestructible;
    public bool IsIndestructible => m_Indestructible;

    /// <summary>
    /// Стартовое кол-во хитпоинтов.
    /// </summary>
    [SerializeField] private int m_HitPoints;

    /// <summary>
    /// Текущие хит поинты
    /// </summary>
    private int m_CurrentHitPoints;
    public int HitPoints => m_CurrentHitPoints;

    [Header("Explosion")]
    [SerializeField] private GameObject m_ExplosionPrefab;

    #endregion

    #region Unity Events

    protected virtual void Start()
    {
        m_CurrentHitPoints = m_HitPoints;
    }

    #endregion

    #region Public API

    /// <summary>
    /// Apply damage to the object
    /// </summary>
    /// <param name="damage"> Damage applied to the object </param>
    public void ApplyDamage(int damage)
    {
        if (m_Indestructible) return;

        // Check for shield system first
        ShieldSystem shieldSystem = GetComponent<ShieldSystem>();
        if (shieldSystem != null && shieldSystem.HasShield)
        {
            float remainingDamage = shieldSystem.AbsorbDamage(damage);
            damage = Mathf.RoundToInt(remainingDamage);
            
            // If all damage was absorbed by shield, don't apply any to hull
            if (damage <= 0) return;
        }

        m_CurrentHitPoints -= damage;

        if (m_CurrentHitPoints <= 0)
            OnDeath();
    }

    #endregion

    /// <summary>
    /// Called when the object's hit points reach zero or below, triggering its destruction.
    /// </summary>
    protected virtual void OnDeath()
    {
        // Instantiate the explosion prefab at the current object's position
        if (m_ExplosionPrefab != null)
        {
            Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        m_EventOnDeath?.Invoke();
    }

    [SerializeField] private UnityEvent m_EventOnDeath;
    public UnityEvent EventOnDeath => m_EventOnDeath;
}