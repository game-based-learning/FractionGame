using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NumberLineFillParticle : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
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
        rb.isKinematic = false;
    }

    public void DeActivatePhysicsResponse()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }
}
