using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pointer : MonoBehaviour
{

    private XRRayInteractor ray;
    private ActionBasedController controller;

    // Start is called before the first frame update
    void Start()
    {
        ray = GetComponent<XRRayInteractor>();
        controller = GetComponent<ActionBasedController>();
        ray.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.selectInteractionState.activatedThisFrame)
        {
            StartRay();
        }
        if (controller.selectInteractionState.deactivatedThisFrame)
        {
            StopRay();
        }
    }

    public void StartRay()
    {
        ray.enabled = true;
    }

    public void StopRay()
    {
        ray.enabled = false;
    }

    
}
