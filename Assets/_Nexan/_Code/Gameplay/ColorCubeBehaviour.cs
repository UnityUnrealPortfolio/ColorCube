using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCubeBehaviour : MonoBehaviour
{
    [SerializeField] Animator cubeAnimator;
    [SerializeField] GameObject m_DestructionFx;
    private void Start()
    {
        GameManager.Instance.OnRandomRotate += HandleRandomRotation;
    }

   

    private void HandleRandomRotation(string _trigger)
    {
        Debug.Log($"Incoming Trigger from Gamemanager: {_trigger}");
        cubeAnimator.SetTrigger(_trigger);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnRandomRotate -= HandleRandomRotation;

    }

    internal void DestroyCube()
    {
        AudioManager.Instance.PlayDestructionFx();
        Destroy(gameObject);
        GameObject vfx = Instantiate(m_DestructionFx);
       
    }
}
