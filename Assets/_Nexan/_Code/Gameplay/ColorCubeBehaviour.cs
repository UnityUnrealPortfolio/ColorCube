using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCubeBehaviour : MonoBehaviour
{
    [SerializeField] Animator cubeAnimator;

    private void Start()
    {
        GameManager.Instance.OnRandomRotate += HandleRandomRotation;
    }

    private void HandleRandomRotation(string _trigger)
    {
        Debug.Log($"Incoming Trigger from Gamemanager: {_trigger}");
        cubeAnimator.SetTrigger(_trigger);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnRandomRotate -= HandleRandomRotation;

    }
}
