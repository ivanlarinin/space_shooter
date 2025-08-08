using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTransform : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    void Update()
    {
        transform.position = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
    }

    public void SetTarget(Transform target)
    {
        m_Target = target;
    }
}