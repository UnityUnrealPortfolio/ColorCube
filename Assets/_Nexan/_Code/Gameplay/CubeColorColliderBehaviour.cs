using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorColliderBehaviour : MonoBehaviour
{
    public Transform m_ColorMeter;
    public MeshRenderer m_ColorMeterRenderer;
    public Material m_DefaultMat;
    private bool m_IsRecharging;
    private void Start()
    {
        m_ColorMeterRenderer.material = m_DefaultMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == gameObject.tag)
        {
            m_ColorMeter.localScale = new Vector3(1, m_ColorMeter.localScale.y+0.1f, 1);
            m_ColorMeterRenderer.material = other.GetComponent<MeshRenderer>().material;

            if(m_ColorMeter.localScale.y >= 1f)
            {
                GameManager.Instance.m_Score += 10;
                m_ColorMeter.localScale = new Vector3(1, 0, 1);
            }
        }
        else if(other.gameObject.tag != gameObject.tag)
        {
            m_ColorMeter.localScale = new Vector3(1, m_ColorMeter.localScale.y - 0.1f, 1);
        
            if(m_ColorMeter.localScale.y <= 0)
            {
                //Destroy the Cube
                Destroy(m_ColorMeter.transform.parent.gameObject);
                GameManager.Instance.Restart();
            }
            m_ColorMeterRenderer.material = other.GetComponent<MeshRenderer>().material;

        }

        other.gameObject.SetActive(false);
    }

  
}
