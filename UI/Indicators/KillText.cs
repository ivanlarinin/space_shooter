using UnityEngine;
using UnityEngine.UI;

public class KillText : MonoBehaviour
{
    private const string KillsTextPrefix = "Kills : ";

    [SerializeField] private Text m_Text;

    private int lastNumKills;

    private void Update()
    {
        int numKills = Player.Instance.NumKills;

        if (lastNumKills != numKills)
        {
            m_Text.text = KillsTextPrefix + numKills.ToString();
            lastNumKills = numKills;
        }
    }
}