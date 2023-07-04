using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private GameColors m_CurrentColor;

    private void Start()
    {
        GameManager.Instance.OnGameColorsChange += HandleActiveGameColorChange;
    }
    private void HandleActiveGameColorChange(GameColors _color)//ToDo:possible duplicate logic here
    {
        switch (_color)
        {
            case GameColors.BLUE:
                m_CurrentColor = GameColors.BLUE;
                break;
            case GameColors.GREEN:
                m_CurrentColor = GameColors.GREEN;
                break;
            case GameColors.RED:
                m_CurrentColor = GameColors.RED;
                break;
            case GameColors.YELLOW:
                m_CurrentColor = GameColors.YELLOW;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("pickup"))
        {
            var pickup = other.gameObject.GetComponent<PickupBehaviour>();
            GameColors pickupColor = pickup.m_CurrentColor;
            if(pickupColor == m_CurrentColor)
            {
                pickup.HandleDestruction();
                GameManager.Instance.TotalScore += 10;//ToDo:magic number
            }
            else
            {
                pickup.BounceOff();
                GameManager.Instance.Health -= GameManager.Instance.HealthDropAmount;
            }
        }
        if (other.gameObject.CompareTag("health"))
        {
            GameManager.Instance.Health += 25;
            var healthBehaviour = other.gameObject.GetComponent<HealthPickUpBehaviour>();
            healthBehaviour.HandleDestroy();
        }
    }
}
