using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpBehaviour : MonoBehaviour
{
    public GameObject healthPickupFX;
    public float spinSpeed;
    private void Update()
    {
        transform.Rotate(Vector3.forward *spinSpeed * Time.deltaTime);
    }
    internal void HandleDestroy()
    {
        var fx =  Instantiate(healthPickupFX);
        Destroy(fx, 1f);
        Destroy(gameObject);
    }


}
