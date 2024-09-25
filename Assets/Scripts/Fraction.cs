public struct Fraction {
    public Fraction(int Numerator, int Denominator) {
        this.numerator = Numerator;
        this.denominator = Denominator;
    }
    public int numerator { get; }
    public int denominator { get; }

    public double value() => numerator / denominator;
}