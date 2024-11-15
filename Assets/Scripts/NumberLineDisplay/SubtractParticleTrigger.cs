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
        //if (toRemove == null)
        //{
        //    toRemove = new List<GameObject>();
        //    Transform parentOfAll = transform.root.Find("FirstNumberLineDisplay").Find("NumberLineFillParent");
        //    foreach (Transform parent in parentOfAll)
        //    {
        //        foreach(Transform child in parent)
        //        {
        //            if (child.name == "NumberLineFillUnit(Clone)") toRemove.Add(child.gameObject);
        //        }
        //    }
        //}
    }

    /* 
     * I changed this function to check the tag of the collider object and just delete the other particle. The
     * Destroy function is unreliable about when objects get destroyed, but I think using that deleted boolean
     * should fix the issue. This should eliminate the need for the list too. I think disabling the object 
     * would also prevent double deletion
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!deleted)
        {
            if (collision.tag == "Positive")
            {
                count++;
                deleted = true;
                Destroy(gameObject);
                Destroy(collision.gameObject);

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
