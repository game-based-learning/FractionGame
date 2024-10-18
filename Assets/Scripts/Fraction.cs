/*
* Public class for Fractions
*/
using UnityEngine;

public class Fraction: MonoBehaviour
{
    [SerializeField] private NumberLine numberLine;

    public void setFraction(int Numerator, int Denominator)
    {
        this.numerator = Numerator;
        this.denominator = Denominator;
        value = (float) Numerator / Denominator;
        numberLine.DisplayInfo(value);
    }

    public int numerator { get; protected set; }
    public int denominator { get; protected set; }
    public float value { get; protected set; }
}