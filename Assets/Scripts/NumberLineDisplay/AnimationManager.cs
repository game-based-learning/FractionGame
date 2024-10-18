using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    //this is just to test the fail success animation, so please copy the appropiate code to appropriate place
    [SerializeField] private Animator animator;


    public void successClicked()
    {
        animator.Play("success_animation");
    }

    public void failClicked()
    {
        animator.Play("fail_animation");
    }
}
