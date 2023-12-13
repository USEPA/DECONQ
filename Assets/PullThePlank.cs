using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullThePlank : MonoBehaviour
{

    float x, y, z;
    private bool incorrect;

    // Start is called before the first frame update
    void Start()
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;
        incorrect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(incorrect && x < -56)
        {
            x = gameObject.transform.position.x + (float)0.02;
            //print(z);
            //gameObject.transform.position.Set(x, y, z);
            gameObject.transform.position = new Vector3(x, y, z);
        }
    }

    public void PullOut()
    {
        incorrect = true;
    }
}
