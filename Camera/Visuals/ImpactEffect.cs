using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private float m_lifetime;
    private float m_Timer;

    private void Update()
    {
        if (m_Timer < m_lifetime)
            m_Timer += Time.deltaTime;
        else
            Destroy(gameObject);
    }
}