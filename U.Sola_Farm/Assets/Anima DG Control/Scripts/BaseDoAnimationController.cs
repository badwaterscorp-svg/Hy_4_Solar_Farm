using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Linq;

public abstract class BaseDoAnimationController : MonoBehaviour
{
    public Vector3 originPosition { get; set; }
    public Vector3 originScale { get; set; }
    public Vector3 originRotation { get; set; }
    protected int currentAnimation =0;
    public int CurrentAnimation { get { return currentAnimation; } private set { } }
    [HideInInspector]
    public List<AnimationAssistant> listAux = new List<AnimationAssistant>();
    [Header("~~~~Events~~~~~")]
    [SerializeField] protected bool useTimeScale = true; 
    [SerializeField] private bool inLoop;
    [SerializeField] protected bool rewindOnDisable;
    public UnityEvent OnStartedCallBack;
    public UnityEvent OnEndedCallBack;
    public event System.Action OnCompleted;

    public abstract void ActiveAnimation(string fromDebug = "");
    public void ActiveAnimation(int newIndex) 
    {
         currentAnimation = newIndex;
         ActiveAnimation("default");
    }
    public abstract void StopAnimations();

    public void RewindAndActiveAnimation()
    {
        Rewind();
        ActiveAnimation("\"default rewind\"");
    }

    protected void OnEnable()
    {
        if (listAux.Count == 0) 
            listAux.Add(new AnimationAssistant());

        if (listAux[currentAnimation].playOnAwake && currentAnimation == 0) 
            ActiveAnimation("default active");
    }

    protected void OnDisable()
    {
        if (rewindOnDisable)
        {
            currentAnimation = 0;
            transform.DOKill();
        }
    }

    public void Rewind() 
    {
        currentAnimation = 0;
        transform.DOKill();
    }


    public List<AnimationAssistant> GetList() => listAux;

    public AnimationAssistant Duplicate(AnimationAssistant aux) => aux.Copy();

    protected void PlusAnimationIndex()
    {
        currentAnimation++;
        if (currentAnimation < listAux.Count)
        {
            if (listAux[currentAnimation].playOnAwake)
                ActiveAnimation("default aumenta animacion");
            
        }
        else
        {
            OnCompleted?.Invoke();
            OnEndedCallBack?.Invoke();
            if (inLoop)
                RewindAndActiveAnimation();
        }
    }

    protected void CallBacks() => PlusAnimationIndex();
    public void SetInloop(bool arg1) => inLoop = arg1;
}

[System.Serializable]
public class AnimationAssistant
{
    public UnityEvent onStarts;
    public UnityEvent onEnds;
    public Vector3 targetPosition;
    public Vector3 targetScale;
    public Vector3 targetRotation;
    public Vector2 targetSizeDelta;
    public float timeAnimation = 0.3F;
    public float delay;
    public float coldTime;
    public Ease animationCurve;
    public bool playOnAwake;
    public Transform atractor;
    public Color colorTarget;
    public bool display;
    public int loops;
    public Sprite spriteShift;
    public float pixelMultiplier;
    public float fadeTarget;
    public RotateMode rotationType = RotateMode.Fast;
    public bool applyOnCanvasGroup;
    public bool displayPosition;
    public bool displayScale;
    public bool displayTexture;
    public bool displayRotation;
    public bool displayColor;
    public bool displayPixelMultiplier;
    public bool displaySizeDelta;
    public bool displayFade;
    public bool displayTextCharaterSplit;
    public bool displayTextWordSplit;
    public bool displayTextLineSplit;
    public float charSplittTarget;
    public float wordSplitTarget;
    public float lineSplitTarget;
#if ANIMA_DOTWEEN_PRO
    public bool displayTextOutlineColor;
    public bool displayTextChange;
    public string newText;
#endif

    public void DisplayAnimationAux()=> display = !display;
    public AnimationAssistant Copy()=>(AnimationAssistant)MemberwiseClone();
}

public enum TypeAnimation
{
    Move,
    MoveLocal,
    MoveReturnOrigin,
    MoveScaleAT,
    MoveWorldPoint,
    MoveWorldPointScale,
    Scale,
    ScaleReturnOriginScale,
    FadeOut,
    FadeIn,
    FadeInScaleAT,
    FadeOutScaleAT,
    SwitchSprite,
    ChangeSprite,
    ColorChange,
    Rotate,
    RotateBackOrigin,
    UIMoveToPoint,
    UIMoveScaleToPoint,
    MoveLocalScaleAT,
    RotateScaleAT,
    MoveLocalFadeInAT,
    SizeDelta,
    PixelPerUnitMultiplier
}


