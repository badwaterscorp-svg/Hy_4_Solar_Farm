using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class AnimationUIController : BaseDoAnimationController
{
    private RectTransform rectTransform;
    private Image image;
    Sequence sequence = null;

    private new void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        base.OnEnable();
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        sequence.Kill();
    }

    public override void StopAnimations() 
    {
        sequence.Kill();
    }

    public override void ActiveAnimation(string origen = "")
    {
        if (currentAnimation == 0)
            OnStartedCallBack?.Invoke();
        sequence = DOTween.Sequence();
        var currentAux = listAux[currentAnimation];
        var delay = currentAux.delay;
        var timeAnim = currentAux.timeAnimation;
        RectTransform atractor = null;
        if (currentAux.atractor != null)
            atractor = (RectTransform)currentAux.atractor.transform;
        else
            atractor = (RectTransform)transform;

        var targetPos = (currentAux.atractor != null) ? (Vector3)atractor.localPosition : currentAux.targetPosition;
        if (currentAux.displayPosition)
            sequence.Insert(0, rectTransform.DOLocalMove(targetPos, timeAnim).
                SetEase(currentAux.animationCurve).SetDelay(delay).
                SetLoops(currentAux.loops));
        if (currentAux.displayScale)
            sequence.Insert(0, rectTransform.DOScale(currentAux.targetScale, timeAnim).
                SetEase(currentAux.animationCurve).SetDelay(delay).
                SetLoops(currentAux.loops));
        if (currentAux.displayTexture)
            sequence.Insert(0, image.DOFade(1, timeAnim).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).SetLoops(currentAux.loops).
                OnComplete(delegate { image.sprite = currentAux.spriteShift; }));
        if (currentAux.displayRotation)
            sequence.Insert(0, rectTransform.DORotate(currentAux.targetRotation, timeAnim, currentAux.rotationType).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops)
                );
        if (currentAux.displayColor)
            sequence.Insert(0, image.DOColor(currentAux.colorTarget, timeAnim).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops)
                );
        if (currentAux.displaySizeDelta)
            sequence.Insert(0, rectTransform.DOSizeDelta(currentAux.targetSizeDelta, timeAnim).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops)
                );

        if (currentAux.displayPixelMultiplier)
            sequence.Insert(0, DOTween.To(() => image.pixelsPerUnitMultiplier, juu => image.pixelsPerUnitMultiplier = juu, currentAux.pixelMultiplier,
                currentAux.timeAnimation).SetEase(currentAux.animationCurve).SetDelay(currentAux.delay).
                SetLoops(currentAux.loops).OnComplete(() => { StopCoroutine(UpdatePixelPerUnit()); }).
                OnPlay(() => StartCoroutine(UpdatePixelPerUnit())));

        if (currentAux.displayFade)
        {
            if (currentAux.applyOnCanvasGroup)
            {
                var canvasGroup = GetComponent<CanvasGroup>();
                sequence.Insert(0, DOTween.To(() => canvasGroup.alpha, juu => canvasGroup.alpha = juu, currentAux.fadeTarget, timeAnim).
                    SetEase(currentAux.animationCurve).
                    SetDelay(currentAux.delay).
                    SetLoops(currentAux.loops));

            }
            else
            {
                sequence.Insert(0, image.DOFade(currentAux.fadeTarget, currentAux.timeAnimation).
                    SetEase(currentAux.animationCurve).SetDelay(delay).
                    SetLoops(currentAux.loops));
            }
        }

        sequence.OnComplete(CallBacks).SetUpdate(!useTimeScale);
    }

    private IEnumerator UpdatePixelPerUnit()
    {
        while (gameObject.activeInHierarchy)
        {
            image.SetAllDirty();
            yield return new WaitForEndOfFrame();
        }
    }

}

