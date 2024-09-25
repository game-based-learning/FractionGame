using UnityEngine;

/*
* Fraction struct contains a numerator and denominator of which are both integers.
* The value is calculated by dividing the numerator by the denominator.
*/
public struct Fraction {
    public Fraction(int Numerator, int Denominator) {
        this.numerator = Numerator;
        this.denominator = Denominator;
        value = Numerator / Denominator;
    }
    public int numerator { get; }
    public int denominator { get; }
    public int value { get; }
}
/*
* Singleton class used to hold an equation's value and solution.
*/
public class EquationManager: MonoBehaviour 
{
    public static EquationManager instance;

    // For now, this class will be hard coded to the problem (x/y) + (z/w) = 1/2
    // Future work on this project will see a way to send a problem to the equation manager based on the puzzle

    // Create private fraction objects to store values into
    private Fraction fraction1;
    private Fraction fraction2;
    private Fraction answer = new Fraction(1, 2);

    // Under the assumption that the equation manager will be notified whenever a submission is made, this function checks the given fractions to the answer
    public void CheckEquation(int n1, int dn1, int n2, int dn2) {
        fraction1 = new Fraction(n1, dn1);
        fraction2 = new Fraction(n2, dn2);
        double totalValue = fraction1.value + fraction2.value;

        // TODO: Ask numberline to draw the sun of the fractions

        if (totalValue == answer.value) {
            // Answer is correct
            // TODO: Do something
        }

        // TODO: Consider what to do when the answer is wrong
    }

    // Return the instance of the equation manager. If the instance does not exist, a new one will be created.
    public EquationManager getInstance() {
        if (instance == null) {
            instance = new EquationManager();
        }
        return instance;
    }
}