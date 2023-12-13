using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{

    public Material sky;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            Rigidbody body = GetComponent<Rigidbody>();
            body.drag = 5;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = 0.06F;
            RenderSettings.skybox = sky;
        }
    }
}
