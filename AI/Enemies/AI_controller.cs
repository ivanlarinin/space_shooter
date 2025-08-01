using System;
using UnityEngine;

[RequireComponent(typeof(SpaceShip))]
public class AIController : MonoBehaviour
{
    public enum AIBehaviour
    {
        Null,
        Patrol
    }

    [SerializeField] private AIBehaviour m_AIBehaviour;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_NavigationLinear;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_NavigationAngular;

    [SerializeField] private float m_RandomSelectMovePointTime;

    [SerializeField] private float m_FindNewTargetTime;

    [SerializeField] private float m_ShootDelay;

    [SerializeField] private float m_EvadeRayLength;

    private SpaceShip m_SpaceShip;

    private Vector3 m_MovePosition;

    private Destructible m_SelectedTarget;

    private void Start()
    {
        m_SpaceShip = GetComponent<SpaceShip>();
        InitTimers();
    }

    private void Update()
    {
        UpdateTimers();
        UpdateAI();
    }

    private void UpdateAI()
    {
        if (m_AIBehaviour == AIBehaviour.Patrol)
        {
            UpdateBehaviouralPatrol();
        }
    }

    private void UpdateBehaviouralPatrol()
    {
        ActionFindNewMovePosition();
        ActionControlShip();
        ActionFindNewAttackTarget();
        ActionFire();
    }

    private void ActionFire()
    {
        throw new NotImplementedException();
    }

    private void ActionFindNewAttackTarget()
    {
        throw new NotImplementedException();
    }

    private void ActionControlShip()
    {
        throw new NotImplementedException();
    }

    private void ActionFindNewMovePosition()
    {
        throw new NotImplementedException();
    }

    #region Timers

    private void InitTimers()
    {

    }

    private void UpdateTimers()
    {
        
    }

    #endregion
}
