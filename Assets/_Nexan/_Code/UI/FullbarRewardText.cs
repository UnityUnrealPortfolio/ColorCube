using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FullbarRewardText : MonoBehaviour
{
    public TMP_Text fullBarRewardTxt;

    private void Start()
    {
        GameManager.Instance.OnFullBarRewardChange += HandleFullBarReward;
        fullBarRewardTxt.text = "00";
    }

    private void HandleFullBarReward(int obj)
    {
        fullBarRewardTxt.text = obj.ToString();
    }
}
