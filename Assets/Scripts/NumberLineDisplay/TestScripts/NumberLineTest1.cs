using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLineTest1 : MonoBehaviour
{
    [SerializeField] private NumberLine numberLine;
    [SerializeField] private List<IndividualNumberLineTest> tests;
    [SerializeField] private float timeForEachTest = 3.0f;

    private float time = 0;
    private int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        RunTest(this.index);
        this.index++;
        if (this.index >= this.tests.Count)
        {
            this.index = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.time >= this.timeForEachTest)
        {
            RunTest(this.index);

            this.index++;
            if (this.index >= this.tests.Count)
            {
                this.index = 0;
            }
            this.time = 0;
        }
        
        this.time += Time.deltaTime;
    }

    private void RunTest(int i)
    {
        this.numberLine.LineRangeMin = this.tests[i].lineRangeMin;
        this.numberLine.LineRangeMax = this.tests[i].lineRangeMax;
        this.numberLine.NumberOfLargeTicks = this.tests[i].numLargeTicks;
        this.numberLine.NumSmallTicksBetweenLargeTicks = this.tests[i].numSmallTicksBetweenLargeTicks;

        this.numberLine.DisplayInfo(this.tests[i].value, this.tests[i].negative);
    }
}

[Serializable]
public class IndividualNumberLineTest
{
    public float value = 0.5f;
    public float lineRangeMin = 0.0f;
    public float lineRangeMax = 1.0f;
    public bool negative = false;
    public int numLargeTicks;
    public int numSmallTicksBetweenLargeTicks;
}
