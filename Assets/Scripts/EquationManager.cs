using UnityEngine;

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

    [Space]
    [SerializeField] private Animator answerAnim;

    private void Awake() 
    {
        instance = this;
    }

    private void OnEnable() 
    {
        //fraction1.updated += CheckEquation;
        //fraction2.updated += CheckEquation;
    }

    // Under the assumption that the equation manager will be notified whenever a submission is made, this function checks the given fractions to the answer
    public void CheckEquation() 
    {
        if (fraction1.value == null || fraction2.value == null)
        {
            return;
        }
        // Calculate the value of the answer fraction
        answer.setFraction((fraction1.numerator * fraction2.denominator + fraction2.numerator * fraction2.denominator), (fraction1.denominator * fraction2.denominator));

        // TODO: Ask numberline to draw the sun of the fractions

        if (answer.value == 0.5) 
        {
            Debug.Log("Correct!");
            answerAnim.Play("success_animation");
        } 
        else 
        {
            Debug.Log("Wrong");
            answerAnim.Play("fail_animation");
        }
        

        // TODO: Consider what to do when the answer is wrong
    }
}
