using UnityEngine;

/// <summary>
/// Provides shield protection for the player ship with visual feedback and regeneration
/// </summary>
public class ShieldSystem : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] private float m_MaxShieldHealth = 100f;
    [SerializeField] private float m_ShieldRegenRate = 10f; // per second
    [SerializeField] private float m_RegenDelay = 3f; // seconds before regen starts
    
    [Header("Visual Effects")]
    [SerializeField] private SpriteRenderer m_ShieldRenderer;
    [SerializeField] private Color m_FullShieldColor = Color.cyan;
    [SerializeField] private Color m_LowShieldColor = Color.red;
    [SerializeField] private float m_ShieldPulseSpeed = 2f;
    
    private float m_CurrentShieldHealth;
    private float m_LastDamageTime;
    private bool m_IsRegenerating;
    private SpaceShip m_SpaceShip;
    
    public float ShieldHealth => m_CurrentShieldHealth;
    public float ShieldHealthPercent => m_CurrentShieldHealth / m_MaxShieldHealth;
    public bool HasShield => m_CurrentShieldHealth > 0f;
    
    private void Start()
    {
        m_CurrentShieldHealth = m_MaxShieldHealth;
        m_SpaceShip = GetComponent<SpaceShip>();
        
        if (m_ShieldRenderer == null)
        {
            m_ShieldRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        
        UpdateShieldVisual();
    }
    
    private void Update()
    {
        HandleShieldRegeneration();
        UpdateShieldVisual();
    }
    
    /// <summary>
    /// Attempts to absorb damage with the shield
    /// </summary>
    /// <param name="damage">Amount of damage to absorb</param>
    /// <returns>Remaining damage after shield absorption</returns>
    public float AbsorbDamage(float damage)
    {
        if (!HasShield) return damage;
        
        m_LastDamageTime = Time.time;
        m_IsRegenerating = false;
        
        float absorbedDamage = Mathf.Min(damage, m_CurrentShieldHealth);
        m_CurrentShieldHealth -= absorbedDamage;
        
        // Visual feedback
        StartCoroutine(ShieldHitEffect());
        
        return damage - absorbedDamage;
    }
    
    /// <summary>
    /// Restores shield health
    /// </summary>
    /// <param name="amount">Amount to restore</param>
    public void RestoreShield(float amount)
    {
        m_CurrentShieldHealth = Mathf.Min(m_CurrentShieldHealth + amount, m_MaxShieldHealth);
    }
    
    private void HandleShieldRegeneration()
    {
        if (Time.time - m_LastDamageTime > m_RegenDelay && m_CurrentShieldHealth < m_MaxShieldHealth)
        {
            m_IsRegenerating = true;
            m_CurrentShieldHealth += m_ShieldRegenRate * Time.deltaTime;
            m_CurrentShieldHealth = Mathf.Min(m_CurrentShieldHealth, m_MaxShieldHealth);
        }
    }
    
    private void UpdateShieldVisual()
    {
        if (m_ShieldRenderer == null) return;
        
        float shieldPercent = ShieldHealthPercent;
        
        // Color interpolation based on shield health
        Color targetColor = Color.Lerp(m_LowShieldColor, m_FullShieldColor, shieldPercent);
        
        // Add pulsing effect when regenerating
        if (m_IsRegenerating && shieldPercent < 1f)
        {
            float pulse = Mathf.Sin(Time.time * m_ShieldPulseSpeed) * 0.3f + 0.7f;
            targetColor.a = pulse;
        }
        else
        {
            targetColor.a = shieldPercent;
        }
        
        m_ShieldRenderer.color = targetColor;
    }
    
    private System.Collections.IEnumerator ShieldHitEffect()
    {
        if (m_ShieldRenderer == null) yield break;
        
        Color originalColor = m_ShieldRenderer.color;
        Color flashColor = Color.white;
        
        // Flash white on hit
        m_ShieldRenderer.color = flashColor;
        yield return new WaitForSeconds(0.1f);
        m_ShieldRenderer.color = originalColor;
    }
} 