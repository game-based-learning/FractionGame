using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtractParticleTrigger : MonoBehaviour
{
    private bool deleted = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!deleted)
        {
            if (collision.name == "NumberLineFillUnit(Clone)")
            {
                if (collision.gameObject.activeInHierarchy && gameObject.activeInHierarchy)
                {
                    deleted = true;
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
