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

    [Space]
    [SerializeField] private NumberLine beaker1;
    [SerializeField] private NumberLine beaker2;

    private TMP_Text textbox;
    private bool beaker1DidOverflow;
    private bool beaker2DidOverflow;

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

        beaker1.beakerOverflow += Beaker1Overflow;
        beaker2.beakerOverflow += Beaker2Overflow;
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

        beaker1.beakerOverflow -= Beaker1Overflow;
        beaker2.beakerOverflow -= Beaker2Overflow;
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
        if (beaker1DidOverflow || beaker2DidOverflow)
            return;

        if ((fraction1.denominator != fraction2.denominator) && fraction1.denominator != 0 && fraction2.denominator != 0)
            SetText("For some puzzles, finding the answer would require one to find a common denominator. This prototype " +
                "doesn't include such functionality, but we plan to implement it in the next iteration.");
        else
            SetText(string.Empty);
    }

    private void Beaker1Overflow(bool didOverflow)
    {
        beaker1DidOverflow = didOverflow;
        if (didOverflow)
            SetText("That value is too large for a beaker of this size!");
        else if (!beaker2DidOverflow)
            SetText(string.Empty);
    }

    private void Beaker2Overflow(bool didOverflow)
    {
        beaker2DidOverflow = didOverflow;
        if (didOverflow)
            SetText("That value is too large for a beaker of this size!");
        else if (!beaker1DidOverflow)
            SetText(string.Empty);
    }

    #endregion

    private void SetText(string text, float fontSize = 16f)
    {
        textbox.text = text;
        textbox.fontSize = fontSize;
    }
}
