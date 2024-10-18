using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static Fraction;
/*
* Singleton class used to hold an equation's value and solution.
*/
public class EquationManager: MonoBehaviour 
{
    public static EquationManager instance;

    // For now, this class will be hard coded to the problem (x/y) + (z/w) = 1/2
    // Future work on this project will see a way to send a problem to the equation manager based on the puzzle

    // Create private fraction objects to store values into
    [SerializeField] private Fraction fraction1;
    [SerializeField] private Fraction fraction2;
    [SerializeField] private Fraction answer;

    private void Awake() {
        instance = this;
    }

    private void OnEnable() {

    }

    private void OnDisable() {

    }

    private void Start() {
     
    }

    // Under the assumption that the equation manager will be notified whenever a submission is made, this function checks the given fractions to the answer
    public void CheckEquation(int n1, int dn1, int n2, int dn2) {
        fraction1.setFraction(n1, dn1);
        fraction2.setFraction(n2, dn2);
        // Calculate the value of the answer fraction
        answer.setFraction((n1 * dn2 + n2 * dn1), (dn1 * dn2));

        // TODO: Ask numberline to draw the sun of the fractions

        if (answer.value == 0.75) {
            Debug.Log("Correct!");
            // Answer is correct
            // TODO: Do something
        } else {
            Debug.Log("Wrong");
        }
        

        // TODO: Consider what to do when the answer is wrong
    }
}
