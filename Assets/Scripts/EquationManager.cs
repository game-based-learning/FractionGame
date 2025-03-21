using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private char operation;

    [Space]
    [SerializeField] private float answerNumerator;
    [SerializeField] private float answerDenominator;

    [Space]
    [SerializeField] private Animator answerAnim;
    [SerializeField] private GameObject opAnimatorObj;

    // Originally I was going to have one event that gave a boolean but I figured I would just make two
    public delegate void OnAnswerChecked();
    public event OnAnswerChecked solvedPuzzle;
    public event OnAnswerChecked failedPuzzle;

    public delegate void OnReset();
    public event OnReset resetState;

    private IOperationAnimator opAnimator;

    private bool isChecking;

    private void Awake() 
    {
        instance = this;

        opAnimator = opAnimatorObj.GetComponent<IOperationAnimator>();
    }

    private void OnEnable() 
    {
        //fraction1.updated += CheckEquation;
        //fraction2.updated += CheckEquation;
        opAnimator.animatedParticles += CheckValueAnimation;
    }

    private void OnDisable()
    {
        opAnimator.animatedParticles -= CheckValueAnimation;
    }

    // Under the assumption that the equation manager will be notified whenever a submission is made, this function checks the given fractions to the answer
    public void CheckEquation() 
    {
        // No checking while animations are playing
        if (isChecking)
            return;

        if (fraction1.value == null || fraction2.value == null)
            return;

        isChecking = true;

        // Calculate the value of the answer fraction
        if (operation == '+')
        {
            answer.setFraction((fraction1.numerator * fraction2.denominator + fraction2.numerator * fraction1.denominator), (fraction1.denominator * fraction2.denominator), false);
        } 
        else
        {
            answer.setFraction((fraction1.numerator * fraction2.denominator - fraction2.numerator * fraction1.denominator), (fraction1.denominator * fraction2.denominator), false);
        }

        // TODO: Ask numberline to draw the sun of the fractions
        
        opAnimator.AnimateOperation(answer.value.Value);        

        // TODO: Consider what to do when the answer is wrong
    }

    private void CheckValueAnimation()
    {
        if (answer.value == answerNumerator / answerDenominator)
        {
            Debug.Log("Correct!");
            answerAnim.Play("success_animation");
            solvedPuzzle?.Invoke();
            isChecking = false;
        }
        else
        {
            Debug.Log("Wrong");
            answerAnim.Play("fail_animation");
            failedPuzzle?.Invoke();

            StartCoroutine(HandleFailState());
        }
    }

    // This is terrible, change this later
    private IEnumerator HandleFailState()
    {
        yield return new WaitForSeconds(1.5f);

        opAnimator.ResetAnimationState();

        fraction1.setFraction(fraction1.numerator, fraction1.denominator);
        fraction2.setFraction(fraction2.numerator, fraction2.denominator);

        answer.setFraction(0, 0);

        resetState?.Invoke();
        isChecking = false;
    }
}
