using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonParent<GameManager>
{
   [field:SerializeField] public float ColorMeterRechargeTime { get; private set; }
    int m_ScoreTotal;
    public event Action<int> OnScoreChange;
    public int m_Score
    {
        get => m_ScoreTotal;
        set
        {
            m_ScoreTotal = value;
            if(m_Score <= 0)
            {
                Debug.Log("Game Over!");
            }
            else
            {
                //ToDo:Notify interested listeners of score change
                OnScoreChange?.Invoke(m_ScoreTotal);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
