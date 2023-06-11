using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public float m_FallSpeed;

    internal void SetFallSpeed(float v)
    {
        m_FallSpeed = v;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * m_FallSpeed * Time.deltaTime);
    }
}
