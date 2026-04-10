using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationTextController : BaseDoAnimationController
{
    private RectTransform _rectTransform;
    private TMP_Text _text;
    Sequence sequence;

    private new void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
        _text = GetComponent<TMP_Text>();
        base.OnEnable();
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        _rectTransform.DOKill();
        _text.DOKill();
    }

    public override void StopAnimations()
    {
        sequence.Kill();
    }

    public override void ActiveAnimation(string fromDebug = "")
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
            sequence.Insert(0, _rectTransform.DOLocalMove(targetPos, timeAnim).
                SetEase(currentAux.animationCurve).SetDelay(delay).
                SetLoops(currentAux.loops));
        if (currentAux.displayScale)
            sequence.Insert(0, _rectTransform.DOScale(currentAux.targetScale, timeAnim).
                SetEase(currentAux.animationCurve).SetDelay(delay).
                SetLoops(currentAux.loops));
        if (currentAux.displayRotation)
            sequence.Insert(0, _rectTransform.DORotate(currentAux.targetRotation, timeAnim, currentAux.rotationType).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops));

#if ANIMA_DOTWEEN_PRO
        if (currentAux.displayColor)
            sequence.Insert(0, _text.DOColor(currentAux.colorTarget, timeAnim).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops));


        if (currentAux.displayTextOutlineColor)
        {
            sequence.Insert(0, _text.DOOutlineColor(currentAux.colorTarget, timeAnim)
                .SetEase(currentAux.animationCurve).SetDelay(delay).SetLoops(currentAux.loops)).OnStart(()=> print("Empieza")).OnComplete(()=>print("Termina"));
            // Don't know why it doesn't work without the line below.
            sequence.Insert(0, _text.DOGlowColor(_text.fontMaterial.GetColor("_GlowColor"), 0));
        }

        if (currentAux.displayTextChange)
        {
            sequence.Insert(0, _text.DOText(currentAux.newText, timeAnim)
                .SetEase(currentAux.animationCurve).SetDelay(delay).SetLoops(currentAux.loops));
        }
#endif
        if (currentAux.displaySizeDelta)
            sequence.Insert(0, _rectTransform.DOSizeDelta(currentAux.targetSizeDelta, timeAnim).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay));


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
#if ANIMA_DOTWEEN_PRO
            else
            {
                sequence.Insert(0, _text.DOFade(currentAux.fadeTarget, currentAux.timeAnimation).
                    SetEase(currentAux.animationCurve).SetDelay(delay).
                    SetLoops(currentAux.loops));
            }
#endif
        }

        if (currentAux.displayTextCharaterSplit)
        {
            sequence.Insert(0, DOTween.To(() => _text.characterSpacing, juu => _text.characterSpacing = juu, currentAux.charSplittTarget, timeAnim).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops));
        }

        if (currentAux.displayTextWordSplit)
        {
            sequence.Insert(0, DOTween.To(() => _text.wordSpacing, juu => _text.wordSpacing = juu, currentAux.wordSplitTarget, timeAnim).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops));
        }

        if (currentAux.displayTextLineSplit)
        {
            sequence.Insert(0, DOTween.To(() => _text.lineSpacing, juu => _text.lineSpacing = juu, currentAux.lineSplitTarget, timeAnim).
                SetEase(currentAux.animationCurve).
                SetDelay(currentAux.delay).
                SetLoops(currentAux.loops));
        }

        sequence.OnComplete(CallBacks).SetUpdate(!useTimeScale);
    }

    
}
