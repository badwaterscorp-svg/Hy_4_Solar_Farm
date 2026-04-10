using DG.Tweening;
using UnityEngine;

#if ANIMA_DOTWEEN_PRO
public class AnimCharDisplayByFall : ITypingAnimaStrategy
{
    public void Animate(DOTweenTMPAnimator animator, float timePerChar, Ease curve)
    {
        Sequence sequence = DOTween.Sequence();
        Sequence sequence2 = DOTween.Sequence();
        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            if (!animator.textInfo.characterInfo[i].isVisible)
                continue;
            Vector3 currCharOffset = animator.GetCharOffset(i);
            sequence.Append(animator.DOOffsetChar(i, currCharOffset - new Vector3(0, 30, 0), timePerChar));
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
            Vector3 currCharOffset = animator.GetCharOffset(i);
            animator.DOFadeChar(i, 0, 0);
            animator.SetCharOffset(i, currCharOffset + new Vector3(0, 30, 0));
        }
    }
}
#endif

#if ANIMA_DOTWEEN_PRO
public class AnimCharFallInvert : ITypingAnimaStrategy
{
    public void Animate(DOTweenTMPAnimator animator, float timePerChar, Ease curve)
    {
        Sequence sequence = DOTween.Sequence();
        Sequence sequence2 = DOTween.Sequence();
        Sequence sequence3 = DOTween.Sequence();
        Debug.Log(animator.textInfo.characterCount - 1);
        for (int i = animator.textInfo.characterCount-1; i >=0; --i)
        {
            if (!animator.textInfo.characterInfo[i].isVisible)
                continue;
            Vector3 currCharOffset = animator.GetCharOffset(i);
            sequence.Append(animator.DOOffsetChar(i, currCharOffset - new Vector3(0, 30, 0), timePerChar));
            sequence2.Append(animator.DOFadeChar(i, 0, timePerChar));
            sequence3.Append(animator.DORotateChar(i, new Vector3(0, 0, Random.Range(-90,90)), timePerChar));
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
            animator.DOFadeChar(i, 1, 0);
            animator.SetCharOffset(i, Vector3.zero);
        }
    }
}
#endif