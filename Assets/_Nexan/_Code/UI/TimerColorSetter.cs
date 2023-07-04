using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets the color of the timer meter to whatever the active cube color is
/// Also sets the fill amount as timer runs out
/// </summary>
public class TimerColorSetter : MonoBehaviour
{
    [SerializeField] Image m_TimerBar;
    private void Start()
    {
        
    }

    private void HandleTimerValueUpdate(float _Value)
    {
        m_TimerBar.fillAmount = _Value;
    }

    private void HandleTimerReset(Color color)
    {
        m_TimerBar.fillAmount = 1f;
        m_TimerBar.color = color;
    }
}
