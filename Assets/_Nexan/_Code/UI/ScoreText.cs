using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
   [SerializeField] TMP_Text m_ScoreText;


    private void Start()
    {
        GameManager.Instance.OnScoreChange += HandleScoreChange;
        GameManager.Instance.OnActiveColorChange += HandleGameColorChange;
        m_ScoreText.text = "00";
    }

    private void HandleGameColorChange(Color obj)
    {
        m_ScoreText.faceColor = obj;
    }

    private void HandleScoreChange(int _score)
    {
        m_ScoreText.text = _score.ToString();
    }
}
