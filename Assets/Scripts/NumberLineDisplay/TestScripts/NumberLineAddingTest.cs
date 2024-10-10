using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLineAddingTest : MonoBehaviour
{
    [SerializeField] private NumberLine mainNumberLine;
    [SerializeField] private NumberLine secondNumberLine;
    [Space]
    [SerializeField] float mainNumberLineValue;
    [SerializeField] float secondNumberLineValue;
    [Space]
    [SerializeField] GameObject operationAnimator;

    private float time = 0;
    private bool shouldAnimate = true;
    
    // Start is called before the first frame update
    void Start()
    {
        mainNumberLine.DisplayInfo(mainNumberLineValue);
        secondNumberLine.DisplayInfo(secondNumberLineValue);
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 2 && shouldAnimate)
        {
            operationAnimator.GetComponent<IOperationAnimator>().AnimateOperation();
            shouldAnimate = false;
        }
    }
}
