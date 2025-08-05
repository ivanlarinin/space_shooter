using UnityEngine;
using UnityEngine.UI;

public class KillText : MonoBehaviour
{
    [SerializeField] private Text m_Text;

    private int lastNumKills;

    private void Update()
    {
        int numKills = Player.Instance.NumKills;

        if (lastNumKills != numKills)
        {
            m_Text.text = "Kills : " + numKills.ToString();
            lastNumKills = numKills;
        }
    }
}