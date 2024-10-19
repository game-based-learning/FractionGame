using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubtractParticleTrigger : MonoBehaviour
{
    private bool deleted = false;
    private static int count = 0;
    private static List<GameObject> toRemove;

    private void Start()
    {
        if (toRemove == null)
        {
            toRemove = new List<GameObject>();
            Transform parentOfAll = transform.root.Find("FirstNumberLineDisplay").Find("NumberLineFillParent");
            foreach (Transform parent in parentOfAll)
            {
                foreach(Transform child in parent)
                {
                    if (child.name == "NumberLineFillUnit(Clone)") toRemove.Add(child.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!deleted)
        {
            if (collision.name == "NumberLineFillUnit(Clone)")
            {
                count++;
                deleted = true;
                Destroy(gameObject);
                Destroy(toRemove[toRemove.Count - count]);

                //this method doesn't delete accurately (meaning, sometimes destroyed object gets destroyed again)
                /* if (!collision.gameObject.IsDestroyed())
                {
                    deleted = true;
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                } */
            }
        }
    }
}
