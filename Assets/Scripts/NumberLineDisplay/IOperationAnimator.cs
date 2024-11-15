public interface IOperationAnimator
{
    //public void AnimateOperation(); <- temporarily changing until add/refresh number line methods are debugged
    void AnimateOperation(float answer);
    void ResetAnimationState();

    public delegate void OnFinished();
    public event OnFinished animatedParticles;
}
