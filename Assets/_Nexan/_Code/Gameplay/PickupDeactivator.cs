using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDeactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "pickup")
        {
            other.gameObject.SetActive(false);
        }
    }
}
