using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public GameColors m_CurrentColor;
    public float m_FallSpeed;
    public float m_MaxSpinSpeed, m_MinSpinSpeed;
    public GameObject m_BluefxPrefab, m_RedfxPrefab, m_GreenFxPrefab, m_YellowFxPrefab;
    private GameObject m_ActiveFxPrefab;
    MeshRenderer m_renderer;

    private void Awake()
    {
        m_renderer = GetComponent<MeshRenderer>();
    }
    internal void SetFallSpeed(float v)
    {
        m_FallSpeed = v;
    }

    internal void SetActiveMaterial(GameColors color)
    {
        m_CurrentColor = color;
        SetActiveFxPrefab(m_CurrentColor);
        m_renderer.material = GameManager.Instance.GetPickupMaterial(m_CurrentColor);
    }

    private void SetActiveFxPrefab(GameColors m_CurrentColor)
    {
        switch (m_CurrentColor)
        {
            case GameColors.BLUE:
                m_ActiveFxPrefab = m_BluefxPrefab;
                break;
            case GameColors.RED:
                m_ActiveFxPrefab = m_RedfxPrefab;
                break;
            case GameColors.GREEN:
                m_ActiveFxPrefab = m_GreenFxPrefab;
                break;
            case GameColors.YELLOW:
                m_ActiveFxPrefab = m_YellowFxPrefab;
                break;
        }
    }

    //ToDo:has no references please check this
    internal void BounceOff()
    {
        AudioManager.Instance.PlayRejectFX();
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-10, 10), 10, 2), ForceMode.Impulse);//ToDo:Magic Numbers
        StartCoroutine(DeactivateInTime());
    }
    private void Update()
    {
        transform.Translate(Vector3.down * m_FallSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * UnityEngine.Random.Range(m_MinSpinSpeed, m_MaxSpinSpeed) * Time.deltaTime);
    }

    public void HandleDestruction()
    {
      
        AudioManager.Instance.PlayPickupFX();
        var fx = Instantiate(m_ActiveFxPrefab, transform.position, Quaternion.identity);
        Destroy(fx, 2f);//ToDo:Magic Numbers
        gameObject.SetActive(false);
    }



    private IEnumerator DeactivateInTime()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
