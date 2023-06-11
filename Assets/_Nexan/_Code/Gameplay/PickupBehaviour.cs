using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public float m_FallSpeed;
    public float m_MaxSpinSpeed, m_MinSpinSpeed;    

    internal void SetFallSpeed(float v)
    {
        m_FallSpeed = v;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * m_FallSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * UnityEngine.Random.Range(m_MinSpinSpeed, m_MaxSpinSpeed) * Time.deltaTime);
    }
}
