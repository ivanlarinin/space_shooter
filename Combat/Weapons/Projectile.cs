using UnityEngine;

public class Projectile : Entity
{
    [SerializeField] private float m_Velocity;
    [SerializeField] private float m_Lifetime;
    [SerializeField] private int m_Damage;
    [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

    private float m_Timer;
    private Destructible m_Parent;

    public void SetParentShooter(Destructible parent)
    {
        m_Parent = parent;
    }

    private void Update()
    {
        float steplength = Time.deltaTime * m_Velocity;
        Vector2 step = transform.up * steplength;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, steplength);

        if (hit)
        {
            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null && dest != m_Parent)
            {
                dest.ApplyDamage(m_Damage);
            }

            OnProjectileLifeEnd(hit.collider, hit.point);
        }

        m_Timer += Time.deltaTime;

        if (m_Timer > m_Lifetime)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(step.x, step.y, 0);
    }

    private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
    {
        Destroy(gameObject);
    }
}