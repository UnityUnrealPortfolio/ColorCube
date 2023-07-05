using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonParent<GameManager>
{

    #region Serialized fields
    [Header("Color Related Variables")]
    [SerializeField] GameColors m_ActiveColor;
    [SerializeField] Color m_CurrentColor;
    [SerializeField] Material m_CurrentPickUpMaterial;
    [SerializeField] Material m_CurrentCubeMaterial;
    [SerializeField] Material m_RedMat, m_GreenMat, m_BlueMat, m_YellowMat;
    [SerializeField] Material m_RedCubeMat, m_GreenCubeMat, m_BlueCubeMat, m_YellowCubeMat;
    [SerializeField] Color m_RedColor, m_GreenColor, m_BlueColor, m_YellowColor;

    [Header("Timer Variables")]
    [SerializeField] float m_MaxTime;
    [SerializeField] float m_timeDropRate;
    [SerializeField] float m_MinDropRate, m_MaxDropRate;
    [SerializeField] float m_RestartTime;
    #endregion

    #region Private Fields
    private Material[] m_PickUpMatArray;
    private Material[] m_CubeMatArray;

    //Timer related fields and props
    float m_CurrentTime;//ToDo:serialized for testing
    float CurrentTime
    {
        get => m_CurrentTime;
        set
        {
            m_CurrentTime = value;
            if (m_CurrentTime < 0)
            {
                ResetTimer();
            }
            OnTimerChange?.Invoke(m_CurrentTime);
        }
    }

    //Score related Fields and Props
    int m_TotalScore;
    public int TotalScore
    {
        get => m_TotalScore;
        set
        {
            m_TotalScore = value;

            OnScoreChange?.Invoke(m_TotalScore);
        }
    }

    //Health related Fields and Props
    [SerializeField] float m_MaxHealth;
    public float MaxHealth { get => m_MaxHealth; }

    [SerializeField] float m_HealthDropAmount;
    public float HealthDropAmount { get => m_HealthDropAmount; }


    [SerializeField] float m_Health;
    public float Health
    {
        get => m_Health;
        set
        {
            m_Health = value;
            if (m_Health <= 0)
            {
                HandlePlayerDeath();
            }
            OnHealthChange?.Invoke(m_Health);
        }
    }

    //Spawn related fields and props
    [SerializeField] float m_PickupSpawnRate;
    public float PickupSpawnRate
    {
        get => m_PickupSpawnRate;
        private set
        {
            m_PickupSpawnRate = value;
            OnPickupSpawnRateChange?.Invoke(m_PickupSpawnRate);

        }
    }

    #endregion

    #region Public Events
    //public events for color change 
    public event Action<Material> OnPickupMaterialChange;
    public event Action<Material> OnCubeMaterialChange;
    public event Action<Color> OnActiveColorChange;
    public event Action<GameColors> OnGameColorsChange;
    public event Action<GameColors> OnGameStart;
    public event Action<float> OnHealthChange;
    public event Action OnPlayerDeath;
    public event Action OnRestart;
    public event Action<float> OnPickupSpawnRateChange;
    //public events for timer changes
    public event Action<float> OnTimerChange;

    //public events for score changes
    public event Action<int> OnScoreChange;
    #endregion

    #region Monobehaviour Callbacks
    protected override void Awake()
    {
        base.Awake();
        m_PickUpMatArray = new Material[] { m_RedMat, m_GreenMat, m_BlueMat, m_YellowMat };
        m_CubeMatArray = new Material[] { m_RedCubeMat, m_YellowCubeMat, m_GreenCubeMat, m_BlueCubeMat };
        CurrentTime = m_MaxTime;
        Health = MaxHealth;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DetermineCurrentColor(m_ActiveColor);
        DetermineCurrentPickUpMaterial(m_ActiveColor);
        DetermineCurrentCubeMaterial(m_ActiveColor);
        
    }

    private void Update()
    {
        CurrentTime -= Time.deltaTime * m_timeDropRate;
    }
    #endregion

    #region Color Utilities
    public Material GetRandomMaterial()
    {
        return m_PickUpMatArray[UnityEngine.Random.Range(0, m_PickUpMatArray.Length)];
    }

    internal Material GetPickupMaterial(GameColors _color)
    {
        Material mat = null;
        switch (_color)
        {
            case GameColors.BLUE:
                mat = m_BlueMat;
                break;
            case GameColors.RED:
                mat = m_RedMat;
                break;
            case GameColors.GREEN:
                mat = m_GreenMat;
                break;
            case GameColors.YELLOW:
                mat = m_YellowMat;
                break;
        }
        return mat;
    }
    private void DetermineCurrentColor(GameColors _color)
    {
        switch (_color)
        {
            case GameColors.BLUE:
                m_CurrentColor = m_BlueColor;
                break;
            case GameColors.RED:
                m_CurrentColor = m_RedColor;
                break;
            case GameColors.GREEN:
                m_CurrentColor = m_GreenColor;
                break;
            case GameColors.YELLOW:
                m_CurrentColor = m_YellowColor;
                break;
        }

        OnActiveColorChange?.Invoke(m_CurrentColor);
        OnGameColorsChange?.Invoke(m_ActiveColor);
    }
    private void DetermineCurrentCubeMaterial(GameColors m_ActiveColor)
    {
        switch (m_ActiveColor)
        {
            case GameColors.BLUE:
                m_CurrentCubeMaterial = m_BlueCubeMat;
                break;
            case GameColors.RED:
                m_CurrentCubeMaterial = m_RedCubeMat;
                break;
            case GameColors.GREEN:
                m_CurrentCubeMaterial = m_GreenCubeMat;
                break;
            case GameColors.YELLOW:
                m_CurrentCubeMaterial = m_YellowCubeMat;
                break;
        }

        OnCubeMaterialChange?.Invoke(m_CurrentCubeMaterial);
    }
    private void DetermineCurrentPickUpMaterial(GameColors m_ActiveColor)
    {
        switch (m_ActiveColor)
        {
            case GameColors.BLUE:
                m_CurrentPickUpMaterial = m_BlueMat;
                break;
            case GameColors.RED:
                m_CurrentPickUpMaterial = m_RedMat;
                break;
            case GameColors.GREEN:
                m_CurrentPickUpMaterial = m_GreenMat;
                break;
            case GameColors.YELLOW:
                m_CurrentPickUpMaterial = m_YellowMat;
                break;
        }

        OnPickupMaterialChange?.Invoke(m_CurrentPickUpMaterial);
    }
    #endregion

    private void ResetTimer()
    {
        //reset CurrentTime
        CurrentTime = m_MaxTime;

        //Randomize the timer drop rate speed
        m_timeDropRate = UnityEngine.Random.Range(m_MinDropRate, m_MaxDropRate);

        //Set the active color to a random color
        int randomColorInt = UnityEngine.Random.Range(0, m_CubeMatArray.Length);
        switch (randomColorInt)
        {
            case 0:
                m_ActiveColor = GameColors.RED;
                break;
            case 1:
                m_ActiveColor = GameColors.BLUE;
                break;
            case 2:
                m_ActiveColor = GameColors.GREEN;
                break;
            case 3:
                m_ActiveColor = GameColors.YELLOW;
                break;
        }
        OnGameColorsChange?.Invoke(m_ActiveColor);
        DetermineCurrentColor(m_ActiveColor);
        DetermineCurrentCubeMaterial(m_ActiveColor);

    }

    private void HandlePlayerDeath()
    {

        OnPlayerDeath?.Invoke();
        AudioManager.Instance.PlayDestructionFX();
        StartCoroutine(ReStart());

    }
    private IEnumerator ReStart()
    {
        LeaderboardsManager.Instance.AddScoreToLeaderboard(TotalScore);
        AnalyticsManager.Instance.SendScoreAtDeathEvent(TotalScore);
        yield return new WaitForSeconds(m_RestartTime);
        OnRestart?.Invoke();
        Health = MaxHealth;
        
        TotalScore = 0;
    }

    internal void SetHealthDrop(float retrievedHealthDrop)
    {
       m_HealthDropAmount = retrievedHealthDrop;
    }

    internal void SetPickupSpawnRate(float retrievedPickupSpawnRate)
    {
        PickupSpawnRate = retrievedPickupSpawnRate;
    }
}
public enum GameColors
{
    BLUE, RED, GREEN, YELLOW
}
