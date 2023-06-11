using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonParent<GameManager>
{
    [field:SerializeField] public float ColorMeterRechargeTime { get; private set; }
    [SerializeField] List<String> randomRotateTriggers;
    [SerializeField] [Range(2,5)]float m_MinRotateInterval;
    [SerializeField][Range(6, 10)] float m_MaxRotateInterval;

    int m_ScoreTotal;
    public event Action<int> OnScoreChange;
    public event Action<string> OnRandomRotate;


    private void Start()
    {
        InvokeRepeating("RotateRandom", 1, UnityEngine.Random.Range( m_MinRotateInterval,m_MaxRotateInterval));//ToDo:magic numbers
    }
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

    private void RotateRandom()
    {
        OnRandomRotate?.Invoke(randomRotateTriggers[UnityEngine.Random.Range(0, randomRotateTriggers.Count)]);
    }
}
