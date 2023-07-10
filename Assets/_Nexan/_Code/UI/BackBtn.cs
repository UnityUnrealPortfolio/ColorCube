using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    public void HandleBackButton()
    {
        SceneController.Instance.HandleBackButton();
    }
}
