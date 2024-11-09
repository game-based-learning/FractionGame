using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackText : MonoBehaviour
{
    [SerializeField] private bool shouldCheckDenominators;
    [SerializeField] private Fraction fraction1;
    [SerializeField] private Fraction fraction2;

    private TMP_Text textbox;

    private void Awake()
    {
        textbox = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        EquationManager.instance.solvedPuzzle += Solved;
        EquationManager.instance.failedPuzzle += Failed;
        EquationManager.instance.resetState += ResetState;

        if (shouldCheckDenominators)
        {
            fraction1.updated += CheckDenominators;
            fraction2.updated += CheckDenominators;
        }
    }

    private void OnDisable()
    {
        EquationManager.instance.solvedPuzzle -= Solved;
        EquationManager.instance.failedPuzzle -= Failed;
        EquationManager.instance.resetState -= ResetState;

        if (shouldCheckDenominators)
        {
            fraction1.updated -= CheckDenominators;
            fraction2.updated -= CheckDenominators;
        }
    }

    private void Start()
    {
        textbox.text = string.Empty;
    }

    #region Listener Methods

    private void Solved()
    {
        SetText("CORRECT!", 24f);
    }

    private void Failed()
    {
        SetText("WRONG!", 24f);
    }
    
    private void ResetState()
    {
        SetText(string.Empty);
    }

    private void CheckDenominators()
    {
        if ((fraction1.denominator != fraction2.denominator) && fraction1.denominator != 0 && fraction2.denominator != 0)
            SetText("For some puzzles, finding the answer would require one to find a common denominator. This prototype " +
                "doesn't include such functionality, but we plan to implement it in the next iteration.");
        else
            SetText(string.Empty);
    }

    #endregion

    private void SetText(string text, float fontSize = 16f)
    {
        textbox.text = text;
        textbox.fontSize = fontSize;
    }
}
