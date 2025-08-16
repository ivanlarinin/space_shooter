using UnityEngine;
using UnityEngine.UI;

public class ShieldText : MonoBehaviour
{
    private const string ShieldTextPrefix = "Shield: ";
    private ShieldSystem m_ShieldSystem;
    private SpaceShip m_BoundShip;

    [SerializeField] private Text m_Text;

    private int lastShieldText = -1;

    private void Start()
    {
        m_ShieldSystem = Player.Instance.ActiveShip.GetComponent<ShieldSystem>();
    }

    private void Update()
    {
        var ship = Player.Instance.ActiveShip;
        if (ship != m_BoundShip)
        {
            m_BoundShip = ship;
            m_ShieldSystem = m_BoundShip ? m_BoundShip.GetComponent<ShieldSystem>() : null;
            lastShieldText = -1;
        }

        UpdateShieldDisplay();
    }

    private void UpdateShieldDisplay()
    {
        if (m_ShieldSystem == null || m_Text == null) return;

        int shieldPercent = Mathf.RoundToInt(m_ShieldSystem.ShieldHealthPercent * 100f);

        if (lastShieldText != shieldPercent)
        {
            m_Text.text = ShieldTextPrefix + shieldPercent + "%";
            lastShieldText = shieldPercent;
        }
    }
}
