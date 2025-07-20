using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : Destructible
{
    /// <summary>
    /// Масса для автоматической установки у ригида.
    /// </summary>
    [Header("Space ship")]
    [SerializeField] private float m_Mass;

    /// <summary>
    /// Толкающая вперед сила.
    /// </summary>
    [SerializeField] private float m_Thrust;

    /// <summary>
    /// Вращающая сила.
    /// </summary>
    [SerializeField] private float m_Mobility;

    /// <summary>
    /// Максимальная линейная скорость.
    /// </summary>
    [SerializeField] private float m_MaxLinearVelocity;

    /// <summary>
    /// Максимальная вращательная скорость. В градусах/сек
    /// </summary>
    [SerializeField] private float m_MaxAngularVelocity;

    [Header("Engine Trail")]
    [SerializeField] private TrailRenderer m_EngineTrail;
    [SerializeField] private float m_MaxTrailTime = 0.5f;
    [SerializeField] private float m_MinTrailTime = 0.1f;
    [SerializeField] private float m_MaxTrailWidth = 0.3f;
    [SerializeField] private float m_MinTrailWidth = 0.05f;
    [SerializeField] private Color m_TrailStartColor = Color.cyan;
    [SerializeField] private Color m_TrailEndColor = Color.clear;

    /// <summary>
    /// Сохраненная ссылка на ригид.
    /// </summary>
    private Rigidbody2D m_Rigid;

    #region Public API

    /// <summary>
    /// Управление линейной тягой. -1.0 до +1.0
    /// </summary>
    public float ThrustControl { get; set; }

    /// <summary>
    /// Управление вращательной тягой. -1.0 до +1.0
    /// </summary>
    public float TorqueControl { get; set; }

    #endregion

    #region Unity Event
    protected override void Start()
    {
        base.Start();

        m_Rigid = GetComponent<Rigidbody2D>();
        m_Rigid.mass = m_Mass;
        m_Rigid.inertia = 1;

        SetupEngineTrail();
    }

    private void Update()
    {
        UpdateEngineTrail();
    }

    private void FixedUpdate()
    {
        UpdateRigidBody();
    }
    #endregion

    /// <summary>
    /// Настройка трейла двигателя
    /// </summary>
    private void SetupEngineTrail()
    {
        if (m_EngineTrail == null)
        {
            // TrailRenderer
            GameObject trailObject = new GameObject("EngineTrail");
            trailObject.transform.SetParent(transform);
            trailObject.transform.localPosition = new Vector3(0.0f,-0.3f,0.0f);
            
            m_EngineTrail = trailObject.AddComponent<TrailRenderer>();
        }

        // Настройка материала трейла
        Material trailMaterial = new Material(Shader.Find("Sprites/Default"));
        trailMaterial.SetColor("_Color", m_TrailStartColor);
        m_EngineTrail.material = trailMaterial;

        // Настройка параметров трейла
        m_EngineTrail.time = 0f;
        m_EngineTrail.startWidth = m_MinTrailWidth;
        m_EngineTrail.endWidth = 0f;
        m_EngineTrail.startColor = m_TrailStartColor;
        m_EngineTrail.endColor = m_TrailEndColor;
        m_EngineTrail.sortingOrder = -1;
    }

    /// <summary>
    /// Обновление трейла двигателя на основе тяги
    /// </summary>
    private void UpdateEngineTrail()
    {
        if (m_EngineTrail == null) return;

        float thrustAmount = Mathf.Max(0, ThrustControl);
        
        if (thrustAmount > 0.01f)
        {
            // Включаем трейл при тяге
            m_EngineTrail.time = Mathf.Lerp(m_MinTrailTime, m_MaxTrailTime, thrustAmount);
            m_EngineTrail.startWidth = Mathf.Lerp(m_MinTrailWidth, m_MaxTrailWidth, thrustAmount);
            
            // Динамически изменяем цвет на основе тяги
            Color currentStartColor = Color.Lerp(m_TrailStartColor * 0.5f, m_TrailStartColor, thrustAmount);
            m_EngineTrail.startColor = currentStartColor;
        }
        else
        {
            // Выключаем трейл когда нет тяги
            m_EngineTrail.time = 0f;
        }
    }

    /// <summary>
    /// Метод добавления сил корабля для движения
    /// </summary>
    private void UpdateRigidBody()
    {
        m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddForce(-m_Rigid.linearVelocity * (m_Mobility / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
    }

}