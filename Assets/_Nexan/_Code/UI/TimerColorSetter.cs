using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// Sets the color of the timer meter to whatever the active cube color is
/// Also sets the fill amount as timer runs out
/// </summary>
public class TimerColorSetter : MonoBehaviour
{
    [SerializeField] Image m_TimerBar;
    private void Awake()
    {
        GameManager.Instance.OnActiveColorChange += HandleActiveColorChange;
        GameManager.Instance.OnTimerChange += HandleTimerValueChange;
        
    }

    private void HandleTimerValueChange(float _Value)
    {
        m_TimerBar.fillAmount = _Value;
    }

    private void HandleActiveColorChange(Color _color)
    {
        
        m_TimerBar.color = _color;
    }


    private void HandleTimerReset(Color color)
    {
        m_TimerBar.fillAmount = 1f;
        m_TimerBar.color = color;
    }
}
