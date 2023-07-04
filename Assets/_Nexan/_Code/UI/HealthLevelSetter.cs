using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthLevelSetter : MonoBehaviour
{
    [SerializeField] Image healthBar;

    private void Start()
    {
        GameManager.Instance.OnActiveColorChange += HandleActiveColorChange;
        GameManager.Instance.OnHealthChange += HandleOnHealthChange;
    }

    private void HandleOnHealthChange(float _value)
    {
        healthBar.fillAmount = _value/GameManager.Instance.MaxHealth;

    }

    private void HandleActiveColorChange(Color _color)
    {
        
        healthBar.color = _color;
    }
}
