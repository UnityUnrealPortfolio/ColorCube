using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCubeRotation : MonoBehaviour
{
    [Header("Spinning controls")]
    [SerializeField] Vector3 m_SpinDirection;
    [SerializeField] float m_SpinSpeed;

    private void Update()
    {
        transform.Rotate(m_SpinDirection * m_SpinSpeed * Time.deltaTime);
    }
}
