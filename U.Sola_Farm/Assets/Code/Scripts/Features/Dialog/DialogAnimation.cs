using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class DialogAnimation
{
    private TMP_Text _textComponent;
    private float _timePerChar;
    private Ease _curve;
    public bool IsRunning;
    ITypingAnimaStrategy animation;

    public DialogAnimation(TMP_Text textComponent, float timePerChar, Ease curve, MonoBehaviour coroutineHost, ITypingAnimaStrategy animation)
    {
        _textComponent = textComponent;
        _timePerChar = timePerChar;
        _curve = curve;
        this.animation = animation;
        animation.OnCompleted += ()=>IsRunning = false;
    }
    ~DialogAnimation() 
    {
        animation.OnCompleted -= ()=>IsRunning = false;
    }

    public void AnimateText(string textNew)
    {
        _textComponent.text = textNew;
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(_textComponent);
        animation.PreAnimate(animator);
        animation.Animate(animator, _timePerChar, _curve);
        IsRunning = true;
    }

    public void ShowFullText()
    {
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(_textComponent);
        animation.CleanAnimations(animator);
        IsRunning = false;
    }

}
