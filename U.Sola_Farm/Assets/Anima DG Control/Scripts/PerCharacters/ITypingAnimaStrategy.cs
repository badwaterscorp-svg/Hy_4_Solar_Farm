using DG.Tweening;

public interface ITypingAnimaStrategy
{
    public void PreAnimate(DOTweenTMPAnimator animator);
    public void Animate(DOTweenTMPAnimator animator,float timePerChar, Ease curve);
    public void CleanAnimations(DOTweenTMPAnimator animator);
}