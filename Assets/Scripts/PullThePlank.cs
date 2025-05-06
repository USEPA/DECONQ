using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullThePlank : MonoBehaviour
{

    float x, y, z;
    private bool incorrect;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;
        incorrect = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(incorrect && x < -56)                    //if the incorrect limit is reached start pulling the plank back
        {
            //until the plank is pulled back to x == -56, shift the x by 0.02 every time update is called
            x = gameObject.transform.position.x + (float)0.02;
            gameObject.transform.position = new Vector3(x, y, z);
        }
    }

    public void PullOut()
    {
        incorrect = true;
        anim.enabled = false;
    }
}
