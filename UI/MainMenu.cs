using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_MainMenuPanel;
    [SerializeField] private GameObject m_ShipSelectionPanel;
    [SerializeField] private GameObject m_LevelSelectionPanel;

    private void Start()
    {
        ShowMainMenuPanel();
    }

    public void ShowMainMenuPanel()
    {
        m_MainMenuPanel.SetActive(true);
        m_ShipSelectionPanel.SetActive(false);
        m_LevelSelectionPanel.SetActive(false);
    }

    public void ShowShipSelectionPanel()
    {
        m_ShipSelectionPanel.SetActive(true);
        m_MainMenuPanel.SetActive(false);
    }

    public void ShowLevelSelectionPanel()
    {
        m_LevelSelectionPanel.SetActive(true);
        m_MainMenuPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}