using DG.Tweening;
using System.Security.Cryptography;

#if ANIMA_DOTWEEN_PRO
public class AnimCharScaleFade : ITypingAnimaStrategy
{
    public void Animate(DOTweenTMPAnimator animator, float timePerChar, Ease curve)
    {
        Sequence sequence = DOTween.Sequence();
        Sequence sequence2 = DOTween.Sequence();
        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            if (!animator.textInfo.characterInfo[i].isVisible)
                continue;
            sequence.Append(animator.DOScaleChar(i,1, timePerChar));
            sequence2.Append(animator.DOFadeChar(i, 1, timePerChar));
        }
    }
    public void CleanAnimations(DOTweenTMPAnimator animator)
    {
        animator.textInfo.ClearAllMeshInfo();
    }

    public void PreAnimate(DOTweenTMPAnimator animator)
    {
        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            if (!animator.textInfo.characterInfo[i].isVisible)
                continue;

            animator.DOFadeChar(i, 0, 0);
            animator.DOScaleChar(i, 10, 0);
        }
    }
}
#endif