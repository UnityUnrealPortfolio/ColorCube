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
    #endregion

    #region Public Events
    //public events for color change 
    public event Action<Material> OnPickupMaterialChange;
    public event Action<Material> OnCubeMaterialChange;
    public event Action<Color> OnActiveColorChange;

    //public events for timer changes
    public event Action<float> OnTimerChange;
    #endregion

    #region Monobehaviour Callbacks
    protected override void Awake()
    {
        base.Awake();
        m_PickUpMatArray = new Material[] { m_RedMat, m_GreenMat, m_BlueMat, m_YellowMat };
        m_CubeMatArray = new Material[] { m_RedCubeMat, m_YellowCubeMat, m_GreenCubeMat, m_BlueCubeMat };
        CurrentTime = m_MaxTime;
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
        DetermineCurrentColor(m_ActiveColor);
        DetermineCurrentCubeMaterial(m_ActiveColor);
        
    }
}
public enum GameColors
{
    BLUE, RED, GREEN, YELLOW
}
