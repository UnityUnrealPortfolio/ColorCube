using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonParent<AudioManager>
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip m_PickupFx;
    [SerializeField] AudioClip m_RejectFX;
    [SerializeField] AudioClip m_CubeDestructionFx;


    private void Start()
    {
        Debug.Log("Start AudioManager");
        
    }

    public void PlayDestructionFX()
    {
        Debug.Log("Inside Playe Destruction FX");
        m_AudioSource.PlayOneShot(m_CubeDestructionFx);
    }

    public void PlayPickupFX()
    {
        m_AudioSource.PlayOneShot(m_PickupFx);
    }

    public void PlayRejectFX()
    {
        m_AudioSource.volume = .3f;
        m_AudioSource.PlayOneShot(m_RejectFX);

    }

}
