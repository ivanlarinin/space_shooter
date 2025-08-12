using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// Уничтожаемый объект на сцене. То что может иметь хит поинты.
/// </summary>
public class Destructable : Entity
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
    public int MaxHitPoints => m_HitPoints;

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


    private static HashSet<Destructable> m_AllDestructibles;

    public static IReadOnlyCollection<Destructable> AllDestructibles => m_AllDestructibles;

    protected virtual void OnEnable()
    {
        if (m_AllDestructibles == null)
        {
            m_AllDestructibles = new HashSet<Destructable>();
        }
        m_AllDestructibles.Add(this);
    }

    protected virtual void OnDestroy()
    {
        m_AllDestructibles.Remove(this);
    }

    public const int TeamIdNeutral = 0;

    [SerializeField] private int m_TeamId;
    public int TeamId => m_TeamId;
    
    /// <summary>
    /// Sets the team ID for this destructible object
    /// </summary>
    /// <param name="teamId">The team ID to assign</param>
    public void SetTeamId(int teamId)
    {
        m_TeamId = teamId;
    }

    [SerializeField] private UnityEvent m_EventOnDeath;
    public UnityEvent EventOnDeath => m_EventOnDeath;

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

    [SerializeField] private int m_ScoreValue;
    public int ScoreValue => m_ScoreValue;
}