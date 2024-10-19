using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLineSubtractionTest : MonoBehaviour
{
    [SerializeField] private NumberLine firstNumberLine, secondNumberLine, answerNumberLine;
    [SerializeField] private float firstValue, secondValue, answerValue;
    [SerializeField] private SubtractionAnimator animator;

    // Start is called before the first frame update
    void Start()
    {
        firstNumberLine.DisplayInfo(firstValue);
        secondNumberLine.DisplayInfo(secondValue);
        answerNumberLine.DisplayInfo(answerValue);
    }

    public void CheckAnswer()
    {
        animator.AnimateOperation(firstValue - secondValue);
    }
}
