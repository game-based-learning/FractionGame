using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLineFillParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    //float time = 0;
    //bool first = false;
    // Update is called once per frame
    void Update()
    {
        /*time += Time.deltaTime;
        if (time > 3 && !first)
        {           
            DeActivatePhysicsResponse();
            this.transform.Translate(new Vector3(0, 4, 0));
            first = true;
        }
        if (time > 6)
        {
            ActivatePhysicsResponse();
            first = false;
            time = 0;
        }*/
    }

    public void ActivatePhysicsResponse()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void DeActivatePhysicsResponse()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
