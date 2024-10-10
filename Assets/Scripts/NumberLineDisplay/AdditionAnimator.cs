using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionAnimator : MonoBehaviour, IOperationAnimator
{
    [SerializeField] private NumberLine mainNumberLine;
    [SerializeField] private NumberLine secondNumberLine;
    [SerializeField] private GameObject additionSign;
    [Space]
    [Tooltip("Where to move the fill out of the secondNumberLine to drop it in the mainNumberLine")]
    [SerializeField] private Vector2 fillDisplacement;
    [SerializeField] private float fillMoveAmount = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateOperation()
    {
        MoveParticlesFromSecondNumberLine();
    }

    private void MoveParticlesFromSecondNumberLine()
    {
        foreach (KeyValuePair<float, GameObject> row in secondNumberLine.FillUnits)
        {
            for (int i = 0; i < row.Value.transform.childCount; i++)
            {
                row.Value.transform.GetChild(i).
                    GetComponent<NumberLineFillParticle>()?.DeActivatePhysicsResponse();
            }
        }

        StartCoroutine(MoveParticles());
    }

    IEnumerator MoveParticles()
    {
        float amountMovedY = 0;
        float unitMoveY = fillDisplacement.y / fillMoveAmount;
        while (amountMovedY <= fillDisplacement.y)
        {
            foreach (KeyValuePair<float, GameObject> row in secondNumberLine.FillUnits)
            {
                row.Value.transform.Translate(0, unitMoveY, 0);              
            }

            amountMovedY += unitMoveY;
            yield return new WaitForSeconds(.1f);
        }
    }

    //TODO: debug first move particles, then move along x on top of main number line,
    //then re-activate physics to drop inside, then de activate 2nd number line and plus sign, then refresh main number line
}
