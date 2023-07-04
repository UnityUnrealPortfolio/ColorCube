using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject m_DeathFX;
    private void Start()
    {
        GameManager.Instance.OnPlayerDeath += HandlePlayerDeath;
        GameManager.Instance.OnRestart += HandleRestart;
    }

    private void HandleRestart()
    {
        gameObject.SetActive(true);
    }

    private void HandlePlayerDeath()
    {
      
       //show deathVFX
       Instantiate(m_DeathFX);

       gameObject.SetActive(false);
       
    }
}
