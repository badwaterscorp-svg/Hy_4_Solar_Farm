using DG.Tweening;
using UnityEngine;

#if ANIMA_DOTWEEN_PRO
public class AnimCharRandomPos : ITypingAnimaStrategy
{
    public void Animate(DOTweenTMPAnimator animator, float timePerChar, Ease curve)
    {
        Sequence sequence = DOTween.Sequence();
        Sequence sequence2 = DOTween.Sequence();
        Sequence sequence3 = DOTween.Sequence();
        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            if (!animator.textInfo.characterInfo[i].isVisible)
                continue;
            sequence.Append(animator.DOScaleChar(i, 1, timePerChar));
            sequence2.Append(animator.DOFadeChar(i, 1, timePerChar));
            sequence3.Append(animator.DOOffsetChar(i, Vector3.zero, timePerChar));
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
            Debug.Log(currCharOffset.ToString()); ;
            animator.DOFadeChar(i, 0.3f, 0);
            animator.DOScaleChar(i, Random.Range(0.4f, 2f), 0);
            animator.DOOffsetChar(i,new Vector3(Random.Range(-30f,30f), Random.Range(-30f, 30f), 0), 0);
        }
    }
}
#endif