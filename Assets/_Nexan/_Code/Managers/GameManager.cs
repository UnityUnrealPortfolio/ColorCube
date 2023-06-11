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
    [SerializeField] float restartDelay;
    [SerializeField] ColorCubeBehaviour m_ColorCube;
    int m_ScoreTotal;
    int m_FullBarReward;
    public event Action<int> OnScoreChange;
    public event Action<int> OnFullBarRewardChange;
    public event Action<string> OnRandomRotate;


    public int FullBarReward
    {
        get => m_FullBarReward;
        set
        {
            m_FullBarReward = value;

            if(m_FullBarReward <= 0)
            {
                m_ColorCube.DestroyCube();
                Restart();
            }
            OnFullBarRewardChange?.Invoke(m_FullBarReward);
        }
    }

    public int m_Score
    {
        get => m_ScoreTotal;
        set
        {
            m_ScoreTotal = value;
            if (m_Score <= 0)
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

    private void Start()
    {
        FullBarReward += 1;
        InvokeRepeating("RotateRandom", 1, UnityEngine.Random.Range( m_MinRotateInterval,m_MaxRotateInterval));//ToDo:magic numbers
    }
   

    public void Restart()
    {
        
        StartCoroutine(RestartInSomeTime());
    }
    private IEnumerator RestartInSomeTime()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    private void RotateRandom()
    {
        OnRandomRotate?.Invoke(randomRotateTriggers[UnityEngine.Random.Range(0, randomRotateTriggers.Count)]);
    }
}
