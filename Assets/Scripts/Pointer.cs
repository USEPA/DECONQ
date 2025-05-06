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
        //turn on or off the laser pointer depending on what state the trigger button is in
        if (controller.selectInteractionState.activatedThisFrame)
        {
            RayStatus(true);
        }
        if (controller.selectInteractionState.deactivatedThisFrame)
        {
            RayStatus(false);
        }
    }

    private void RayStatus(bool status)
    {
        ray.enabled = status;
    }
}
