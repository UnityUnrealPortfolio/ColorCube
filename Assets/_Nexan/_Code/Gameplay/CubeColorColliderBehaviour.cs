using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorColliderBehaviour : MonoBehaviour
{
    public Transform m_ColorMeter;
    public MeshRenderer m_ColorMeterRenderer;
    public Material m_DefaultMat;
    private bool m_IsRecharging;
    public GameObject m_DestructionFx;
    
    private void Start()
    {
        m_ColorMeterRenderer.material = m_DefaultMat;
        m_ColorMeter.localScale = new Vector3(1, 0.3f, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == gameObject.tag)
        {
            m_ColorMeter.localScale = new Vector3(1, m_ColorMeter.localScale.y+0.1f, 1);
            m_ColorMeterRenderer.material = other.GetComponent<MeshRenderer>().material;
            GameManager.Instance.m_Score += 10;
            if (m_ColorMeter.localScale.y >= 1f)
            {
                GameManager.Instance.FullBarReward += 1;
                m_ColorMeter.localScale = new Vector3(1, 0, 1);
            }
           other.gameObject.SetActive(false);
        }
        else if(other.gameObject.tag != gameObject.tag)
        {
            m_ColorMeter.localScale = new Vector3(1, m_ColorMeter.localScale.y - 0.1f, 1);
            m_ColorMeterRenderer.material = m_DefaultMat;
            if(m_ColorMeter.localScale.y <= 0)
            {
                //Destroy the Cube
               GameManager.Instance.FullBarReward -= 1;
            }
            
            other.gameObject.GetComponent<PickupBehaviour>().BounceOff();
        }

    }

  
}
