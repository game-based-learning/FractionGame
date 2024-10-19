using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLineAddingTest : MonoBehaviour
{
    [SerializeField] private NumberLine firstNumberLine;
    [SerializeField] private NumberLine secondNumberLine;
    [SerializeField] private NumberLine answerNumberLine;
    [Space]
    [SerializeField] float firstNumberLineValue;
    [SerializeField] float secondNumberLineValue;
    [SerializeField] float answerNumberLineValue;
    [Space]
    [SerializeField] GameObject operationAnimator;

    private float time = 0;
    private bool shouldAnimate = true;
    
    // Start is called before the first frame update
    void Start()
    {
        firstNumberLine.DisplayInfo(firstNumberLineValue);
        secondNumberLine.DisplayInfo(secondNumberLineValue);
        answerNumberLine.DisplayInfo(answerNumberLineValue);
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 2 && shouldAnimate)
        {
            float answer = firstNumberLineValue + secondNumberLineValue;
            operationAnimator.GetComponent<IOperationAnimator>().AnimateOperation(answer);
            shouldAnimate = false;
        }
    }
}
