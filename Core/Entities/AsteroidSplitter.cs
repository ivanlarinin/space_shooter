using UnityEngine;

/// <summary>
/// Component that makes asteroids split into smaller pieces when destroyed
/// </summary>
public class AsteroidSplitter : MonoBehaviour
{
    [Header("Asteroid Splitting")]
    [SerializeField] private Destructable[] m_SmallerDebrisPrefabs;
    [SerializeField] private int m_SplitCount = 2;
    [SerializeField] private float m_SplitForce = 3f;

    private Destructable m_Destructible;

    private void Start()
    {
        m_Destructible = GetComponent<Destructable>();
        if (m_Destructible != null)
        {
            m_Destructible.EventOnDeath.AddListener(OnAsteroidDestroyed);
        }
    }

    /// <summary>
    /// Called when the asteroid is destroyed
    /// </summary>
    private void OnAsteroidDestroyed()
    {
        if (m_SmallerDebrisPrefabs != null && m_SmallerDebrisPrefabs.Length > 0)
        {
            SplitIntoSmallerDebris();
        }
    }

    /// <summary>
    /// Splits the asteroid into smaller debris pieces
    /// </summary>
    private void SplitIntoSmallerDebris()
    {
        for (int i = 0; i < m_SplitCount; i++)
        {
            // Randomly select a smaller debris prefab
            int prefabIndex = Random.Range(0, m_SmallerDebrisPrefabs.Length);
            Destructable smallerDebris = Instantiate(m_SmallerDebrisPrefabs[prefabIndex]);
            
            // Position slightly offset from the original
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            smallerDebris.transform.position = transform.position + (Vector3)randomOffset;
            
            // Add random velocity
            Rigidbody2D rb = smallerDebris.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.linearVelocity = randomDirection * m_SplitForce;
            }
        }
    }
} 