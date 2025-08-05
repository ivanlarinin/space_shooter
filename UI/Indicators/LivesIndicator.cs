using UnityEngine;
using UnityEngine.UI;   

public class LivesIndicator : MonoBehaviour
{
    [SerializeField] private Text m_Text;

    private int lastLives;

    private void Update()
    {
        int lives = Player.Instance.NumLives;

        if (lastLives != lives)
        {
            m_Text.text = lives.ToString();
            lastLives = lives;
        }
    }
}
