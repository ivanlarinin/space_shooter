using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI component that displays shield status
/// </summary>
public class ShieldUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider m_ShieldSlider;
    [SerializeField] private Image m_ShieldFillImage;
    [SerializeField] private Text m_ShieldText;
    
    [Header("Visual Settings")]
    [SerializeField] private Color m_FullShieldColor = Color.cyan;
    [SerializeField] private Color m_LowShieldColor = Color.red;
    [SerializeField] private Color m_RegeneratingColor = Color.yellow;
    
    private ShieldSystem m_ShieldSystem;
    
    private void Start()
    {
        // Find the player's shield system
        if (Player.Instance != null && Player.Instance.ActiveShip != null)
        {
            m_ShieldSystem = Player.Instance.ActiveShip.GetComponent<ShieldSystem>();
        }
        
        if (m_ShieldSlider != null)
        {
            m_ShieldSlider.minValue = 0f;
            m_ShieldSlider.maxValue = 1f;
        }
    }
    
    private void Update()
    {
        if (m_ShieldSystem == null) return;
        
        UpdateShieldDisplay();
    }
    
    private void UpdateShieldDisplay()
    {
        float shieldPercent = m_ShieldSystem.ShieldHealthPercent;
        
        // Update slider
        if (m_ShieldSlider != null)
        {
            m_ShieldSlider.value = shieldPercent;
        }
        
        // Update text
        if (m_ShieldText != null)
        {
            m_ShieldText.text = $"Shield: {m_ShieldSystem.ShieldHealth:F0}/100";
        }
        
        // Update fill color
        if (m_ShieldFillImage != null)
        {
            Color targetColor;
            
            if (shieldPercent > 0.5f)
            {
                targetColor = m_FullShieldColor;
            }
            else if (shieldPercent > 0.2f)
            {
                targetColor = m_LowShieldColor;
            }
            else
            {
                targetColor = m_RegeneratingColor;
            }
            
            m_ShieldFillImage.color = targetColor;
        }
    }
} 