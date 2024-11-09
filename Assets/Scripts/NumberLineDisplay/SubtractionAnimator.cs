using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtractionAnimator : MonoBehaviour, IOperationAnimator
{
    [SerializeField] private NumberLine firstNumberLine;
    [SerializeField] private NumberLine secondNumberLine;
    [SerializeField] private NumberLine answerNumberLine;
    [SerializeField] private GameObject subtractionSign;
    [SerializeField] private GameObject equalSign;

    [Space]
    [Tooltip("Where to move the fill out of the firstNumberLine to drop it in the answerNumberLine")]
    [SerializeField] private Vector2 firstfillDisplacement;
    [Tooltip("Where to move the fill out of the secondNumberLine to drop it in the answerNumberLine")]
    [SerializeField] private Vector2 secondfillDisplacement;
    [SerializeField] private float fillMoveAmount = 0.1f;

    [Space]
    [Tooltip("Time Before DeActivating everything except the Answer Number Line after moving particles")]
    [SerializeField] private float timeBeforeRemove;
    [Tooltip("Time Before Resetting the Answer Number Line after moving the particles")]
    [SerializeField] private float timeBeforeReset;

    public event IOperationAnimator.OnFinished animatedParticles;

    public void AnimateOperation(float answer)
    {
        StartCoroutine(MoveParticles(answer));
    }

    IEnumerator MoveParticles(float answer)
    {
        DeActivatePhysics(secondNumberLine);
        DeActivatePhysics(firstNumberLine);

        //Move Particles to Answer Number Line
        //yield return to wait until first move particles has completed
        yield return StartCoroutine(MoveParticlesFrom(firstNumberLine, firstfillDisplacement));
        //yield return to wait until second move particles has completed
        yield return StartCoroutine(MoveParticlesFrom(secondNumberLine, secondfillDisplacement));      

        StartCoroutine( DeActivateObjectsOtherThanAnswerLine() );

        //Reset the Answer Number Line
        yield return new WaitForSeconds(timeBeforeReset);

        //answerNumberLine.Add(firstNumberLine); <- Doesn't work
        //answerNumberLine.Add(secondNumberLine); <- Doesn't work

        firstNumberLine.NumberLineFillParent.SetActive(false);
        secondNumberLine.NumberLineFillParent.SetActive(false);

        answerNumberLine.DisplayInfo(answer); //remove if can get adding/refresh working

        //answerNumberLine.RefreshInfo(); <- Doesn't work
    }

    private void DeActivatePhysics(NumberLine numberLine)
    {
        foreach (KeyValuePair<float, GameObject> row in numberLine.FillUnits)
        {
            for (int i = 0; i < row.Value.transform.childCount; i++)
            {
                row.Value.transform.GetChild(i).
                    GetComponent<NumberLineFillParticle>()?.DeActivatePhysicsResponse();
            }
        }
    }

    IEnumerator MoveParticlesFrom(NumberLine numberLine, Vector2 fillDisplacement)
    {
        //Move Particles Up
        float amountMovedY = 0;
        while (amountMovedY <= fillDisplacement.y)
        {
            foreach (KeyValuePair<float, GameObject> row in numberLine.FillUnits)
            {
                row.Value.transform.Translate(0, fillMoveAmount, 0);              
            }

            amountMovedY += fillMoveAmount;
            yield return new WaitForSeconds(.01f);
        }
        Debug.Log("amountMovedY: " + amountMovedY + ", fillDisplacement.y: " + fillDisplacement.y);

        //Move Particles sideways
        float amountMovedX = 0;
        if (fillDisplacement.x >= 0)
        {
            while (amountMovedX <= fillDisplacement.x)
            {
                foreach (KeyValuePair<float, GameObject> row in numberLine.FillUnits)
                {
                    row.Value.transform.Translate(fillMoveAmount, 0, 0);
                }

                amountMovedX += fillMoveAmount;
                yield return new WaitForSeconds(.01f);
            }
        }
        else
        {
            while (amountMovedX >= fillDisplacement.x)
            {
                foreach (KeyValuePair<float, GameObject> row in numberLine.FillUnits)
                {
                    row.Value.transform.Translate(-fillMoveAmount, 0, 0);
                }

                amountMovedX -= fillMoveAmount;
                yield return new WaitForSeconds(.01f);
            }
        }      
        Debug.Log("amountMovedX: " + amountMovedX + ", fillDisplacement.x: " + fillDisplacement.x);

        //Drop Particles (re-activate physics)
        foreach (KeyValuePair<float, GameObject> row in numberLine.FillUnits)
        {
            for (int i = 0; i < row.Value.transform.childCount; i++)
            {
                row.Value.transform.GetChild(i).
                    GetComponent<NumberLineFillParticle>()?.ActivatePhysicsResponse();
            }
        }
    }

    //Separate co-routine in case we want to make this a separate animation later
    IEnumerator DeActivateObjectsOtherThanAnswerLine()
    {
        //Run animation
        yield return new WaitForSeconds(timeBeforeRemove);

        DeActivateLine(firstNumberLine);
        DeActivateLine(secondNumberLine);
        
        subtractionSign.SetActive(false);
        equalSign.SetActive(false);
    }

    private void DeActivateLine(NumberLine line)
    {
        //Set all children of line except NumberLineFillParent to not active
        for (int i = 0; i < line.gameObject.transform.childCount; i++)
        {
            GameObject childObject = line.gameObject.transform.GetChild(i).gameObject;
            if (childObject != line.NumberLineFillParent)
            {
                childObject.SetActive(false);
            }
        }
    }

    public void ResetAnimationState()
    {
        //subtractionSign.SetActive(false);
        //equalSign.SetActive(false);

        firstNumberLine.NumberLineFillParent.SetActive(true);
        secondNumberLine.NumberLineFillParent.SetActive(true);
    }
}
