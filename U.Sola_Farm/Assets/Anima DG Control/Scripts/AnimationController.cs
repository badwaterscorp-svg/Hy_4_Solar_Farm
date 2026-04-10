using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AnimationController : BaseDoAnimationController
{
    private SpriteRenderer _spriteRender;
    private Transform _transform;
    Sequence sequence = null;
    private new void OnEnable()
    {
        _transform = transform;
        if (GetComponent<SpriteRenderer>())
            _spriteRender = GetComponent<SpriteRenderer>();

        originPosition = _transform.position;
        originScale = _transform.localScale;
        base.OnEnable();
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        _spriteRender.DOKill();
    }

    public override void StopAnimations()
    {
        sequence.Kill();
    }

    public override void ActiveAnimation(string origin = "")
    {
        if (currentAnimation == 0)
        {
            OnStartedCallBack?.Invoke();
        }

        sequence = DOTween.Sequence();
        var currentAux = listAux[currentAnimation];
        var delay = currentAux.delay;
        var timeAnim = currentAux.timeAnimation;
        var targetPos = (currentAux.atractor != null) ? (Vector3)currentAux.atractor.localPosition : currentAux.targetPosition;
        if (currentAux.displayPosition)
        {
            if (currentAux.displayPosition)
                sequence.Insert(0, _transform.DOLocalMove(targetPos, timeAnim).
                    SetEase(currentAux.animationCurve).SetDelay(delay).
                    SetLoops(currentAux.loops).SetUpdate(!useTimeScale));
        }

        if (currentAux.displayScale)
            sequence.Insert(0, _transform.DOScale(currentAux.targetScale, timeAnim).
                SetEase(currentAux.animationCurve).SetDelay(delay).
                SetLoops(currentAux.loops).SetUpdate(!useTimeScale));

        if (currentAux.displayRotation)
            sequence.Insert(0, _transform.DORotate(currentAux.targetRotation, timeAnim, currentAux.rotationType).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops)
                .SetUpdate(!useTimeScale));

        sequence.OnComplete(CallBacks);
    }
}
