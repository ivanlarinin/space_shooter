using UnityEngine;

public class SpaceShip : MonoBehaviour
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

    /// <summary>
    /// Сохраненная ссылка на ригид.
    /// </summary>
    private Rigidbody2D m_Rigid;

    #region Public API
}