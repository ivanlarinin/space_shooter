using UnityEngine;
using UnityEngine.UI;

public class HitPointBar : MonoBehaviour
{
    [SerializeField] private Image m_Image;

    private float lastHitPoints;

    private void Update()
    {
        float hitPoints = (float)Player.Instance.ActiveShip.HitPoints / (float)Player.Instance.ActiveShip.MaxHitPoints;

        if (hitPoints != lastHitPoints)
        {
            m_Image.fillAmount = hitPoints;
            lastHitPoints = hitPoints;
        }
    }
}