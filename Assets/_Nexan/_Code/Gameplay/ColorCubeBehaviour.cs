using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCubeBehaviour : MonoBehaviour
{

    [SerializeField] MeshRenderer m_Renderer;
    [SerializeField] Animator cubeAnimator;

    //ToDo:serialized for testing
    private void Start()
    {
        GameManager.Instance.OnCubeMaterialChange += HandleActiveMaterialChange;
       
    }


    private void HandleActiveMaterialChange(Material _mat)
    {
        m_Renderer.material = _mat;
        RandomFlip();
    }

    private void RandomFlip()
    {
        int randomFlipInt = UnityEngine.Random.Range(0, 3);
        switch (randomFlipInt)
        {
            case 0:
                cubeAnimator.SetTrigger("flip1");
                break;
            case 1:
                cubeAnimator.SetTrigger("flip2");
                break;
            case 2:
                cubeAnimator.SetTrigger("flip3");
                break;
        }
    }
}
