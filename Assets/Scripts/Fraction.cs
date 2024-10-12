/*
* Public class for Fractions
*/
public class Fraction 
{
    public Fraction(int Numerator, int Denominator) {
        this.numerator = Numerator;
        this.denominator = Denominator;
        value = Numerator / Denominator;
    }
    public int numerator { get; }
    public int denominator { get; }
    public int value { get; }
}