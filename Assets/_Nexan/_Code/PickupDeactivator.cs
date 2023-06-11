using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDeactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "red" || other.tag == "blue"
            || other.tag =="green" || other.tag == "yellow")
        {
            other.gameObject.SetActive(false);
        }
    }
}
