using UnityEngine;

public class ExplosionLifetime : MonoBehaviour
{
    [SerializeField] private float m_Lifetime = 0.5f;
    void Start()
    {
        Destroy(gameObject, m_Lifetime);
    }
}