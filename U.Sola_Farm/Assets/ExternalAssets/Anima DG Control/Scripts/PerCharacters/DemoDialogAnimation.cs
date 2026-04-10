using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class DemoDialogAnimation : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;
    [SerializeField] float timePerChar =0.2f;
    [SerializeField] Ease curve;
    Coroutine animationRoutine = null;
#if ANIMA_DOTWEEN_PRO
    public void AnimateText(ITypingAnimaStrategy animation, string textNew) 
    {
        if (animationRoutine == null)
            animationRoutine = StartCoroutine(DoAnimateText(animation, textNew));
        else
            StopCoroutine(animationRoutine);
    }

    IEnumerator DoAnimateText(ITypingAnimaStrategy animation,string textNew)
    {
        textComponent.text = textNew;
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(textComponent);
        animation.PreAnimate(animator);
        yield return new WaitForSeconds(0.3f);
        animation.Animate(animator, timePerChar,curve);
    }
#endif
}



