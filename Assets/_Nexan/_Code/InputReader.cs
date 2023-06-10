using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    //swiping left turns the cube -90 degrees in z axes
    //swiping right turns the cube +90 degrees in z axes
    //swiping up turns the cube +90 degrees in x axes
    //swiping down turns the cube -90 degrees in x axes
    //long press on left of screen moves the cube to the left
    //long press on right of screen moves the cube the right

    public float leftSwipeThreshold, rightSwipeThreshold;
    public float upSwipThreshold, downSwipThreshold;
    public float lerpAmount;
    public float m_moveSpeed;
   

    public RotationState rotationState;
    public Transform ColorCubeTransform;
    public Transform ColorCubeHolder;


    Vector2 m_StartTouchPosition;
    Vector2 m_EndTouchPosition;
    [SerializeField]float currentZRot = 0;

    private void Start()
    {
    }
    private void Update()
    {
        MoveCube();
        RotateCube();
        HandleRotationStates();
    }

    private void MoveCube()
    {
        if(Input.GetMouseButton(0))
        {
            if(Input.mousePosition.x < Screen.width/2) 
            {
                ColorCubeHolder.Translate(Vector3.left * m_moveSpeed * Time.deltaTime);
            }
            else if(Input.mousePosition.x > Screen.width/2)
            {
                ColorCubeHolder.Translate(Vector3.right * m_moveSpeed * Time.deltaTime);

            }
        }
    }

    private void HandleRotationStates()
    {
        switch (rotationState)
        {

            case RotationState.RIGHT:

             
                ColorCubeTransform.rotation = Quaternion.Lerp(ColorCubeTransform.rotation, Quaternion.Euler(0, 0, currentZRot - 90), lerpAmount);
                if (Mathf.Round(ColorCubeTransform.eulerAngles.z) == Mathf.Round(currentZRot - 90))
                {
                    rotationState = RotationState.NONE;
                    currentZRot -= 90;

                    if (Mathf.Round(currentZRot) <= 0)
                    {
                        currentZRot = 360;
                    }
                }
                //currentZRot += 90;
                break;

            case RotationState.LEFT:

                if (currentZRot == 360)
                {
                    currentZRot = 0;
                }
                ColorCubeTransform.localRotation = Quaternion.Lerp(ColorCubeTransform.localRotation, Quaternion.Euler(0, 0, currentZRot + 90), lerpAmount);

                if (Mathf.Round(ColorCubeTransform.eulerAngles.z) == Mathf.Round(currentZRot + 90))
                {
                    rotationState = RotationState.NONE;
                    currentZRot += 90;
                    if (Mathf.Round(currentZRot) >= 360)
                    {

                        currentZRot = 0;
                    }

                }
                break;
        }
    }

    private void RotateCube()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                m_StartTouchPosition = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                m_EndTouchPosition = touch.position;

                var deltaTouch = m_EndTouchPosition - m_StartTouchPosition;

                if (deltaTouch.x > rightSwipeThreshold)
                {
                    if (deltaTouch.y > downSwipThreshold || deltaTouch.y < upSwipThreshold)
                    {
                        rotationState = RotationState.RIGHT;

                    }
                }
                else if (deltaTouch.x < leftSwipeThreshold)
                {
                    if (deltaTouch.y > downSwipThreshold || deltaTouch.y < upSwipThreshold)
                    {
                        rotationState = RotationState.LEFT;
                    }

                }
             
            }
        }
    }
}
public enum RotationState
{
    RIGHT,
    LEFT,
    NONE
}
