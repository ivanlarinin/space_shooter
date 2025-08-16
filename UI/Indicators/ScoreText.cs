using UnityEngine;
using UnityEngine.UI;   

public class ScoreText : MonoBehaviour
{
    private const string ScoreTextPrefix = "Score: ";

    [SerializeField] private Text m_Text;

    private int lastScoreText;

    private void Update()
    {
        int score = Player.Instance.Score;

        if (lastScoreText != score)
        {
            m_Text.text = ScoreTextPrefix + score.ToString();
            lastScoreText = score;
        }
    }
}
