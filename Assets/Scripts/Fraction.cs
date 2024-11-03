/*
* Public class for Fractions
*/
using UnityEngine;

public class Fraction: MonoBehaviour
{
    [SerializeField] private NumberLine numberLine;
    [SerializeField] private CardSlot numeratorSlot;
    [SerializeField] private CardSlot denominatorSlot;

    public delegate void OnUpdate();
    public event OnUpdate updated;

    public int numerator { get; protected set; }
    public int denominator { get; protected set; }
    public float? value { get; protected set; }

    public void Awake()
    {
        numerator = 0;
        denominator = 0;
        updateValue();
    }

    public void OnEnable()
    {
        if (numeratorSlot == null && denominatorSlot == null) return;
        numeratorSlot.cardSlotted += updateNumerator;
        denominatorSlot.cardSlotted += updateDenominator;
    }

    private void updateNumerator(Card card)
    {
        if (card == null) return;
        numerator = card.value;
        updateValue();
    }

    private void updateDenominator(Card card)
    {
        if (card == null) return;
        denominator = card.value;
        updateValue();
    }
    
    public void setFraction(int Numerator, int Denominator)
    {
        this.numerator = Numerator;
        this.denominator = Denominator;
        updateValue();
    }

    private void updateValue()
    {
        value = (denominator != 0) ? ((float)numerator / denominator) : null;
        // If value is null, display 0
        numberLine.DisplayInfo(value ?? 0);
        updated?.Invoke();
    }
}