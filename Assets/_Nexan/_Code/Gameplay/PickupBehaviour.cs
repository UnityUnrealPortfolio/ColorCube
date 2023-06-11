using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public float m_FallSpeed;
    public float m_MaxSpinSpeed, m_MinSpinSpeed;
    public GameObject m_fxPrefab;

    internal void SetFallSpeed(float v)
    {
        m_FallSpeed = v;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * m_FallSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * UnityEngine.Random.Range(m_MinSpinSpeed, m_MaxSpinSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            AudioManager.Instance.PlayPickupFX();
            var fx = Instantiate(m_fxPrefab, transform.position, Quaternion.identity);
            Destroy(fx, 2f);//ToDo:Magic Numbers
        }

    }

    internal void BounceOff()
    {
        AudioManager.Instance.PlayRejectFX();
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-10, 10), 10, 2), ForceMode.Impulse);//ToDo:Magic Numbers
        StartCoroutine(DeactivateInTime());
    }

    private IEnumerator DeactivateInTime()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
