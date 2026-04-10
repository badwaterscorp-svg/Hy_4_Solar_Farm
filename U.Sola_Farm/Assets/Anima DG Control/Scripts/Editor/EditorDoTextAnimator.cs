using UnityEngine;
using UnityEditor;
using DG.Tweening;



#if UNITY_EDITOR
[CustomEditor(typeof(AnimationTextController))]
public class EditorDoTextAnimator : BaseEditorAnimator
{
    //protected new AnimationUIController animationController;
    public override void OnInspectorGUI()
    {
        animationController = (AnimationTextController)target;
        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(animationController);
        }
    }

    private SerializedProperty animationsProperty;
    protected override void ShowData(AnimationAssistant animationAux)
    {
        //animationAux.animationType = (TypeAnimation)EditorGUILayout.EnumPopup("Animation Type", animationAux.animationType);
        animationAux.delay = EditorGUILayout.FloatField("Delay Time", animationAux.delay);
        animationAux.timeAnimation = EditorGUILayout.FloatField("Time Animation", animationAux.timeAnimation);
        animationAux.animationCurve = (Ease)EditorGUILayout.EnumPopup("Animation Type", animationAux.animationCurve);
        animationAux.loops = EditorGUILayout.IntField("Loops", animationAux.loops);
        animationAux.playOnAwake = EditorGUILayout.Toggle("Play On Awake", animationAux.playOnAwake);
        ShowTargetPosition(animationAux);
        ShowTargetScale(animationAux);
        ShowTargetRotation(animationAux);
        ShowSizeDelta(animationAux);
        ShowFade(animationAux);
        ShowCharacterSplit(animationAux);
        ShowWordSplit(animationAux);
        ShowLineSeparation(animationAux);
#if ANIMA_DOTWEEN_PRO
        ShowNewText(animationAux);
        ShowColorOutline(animationAux);
        ShowColor(animationAux);
#endif
    }

    protected override void ShowTargetPosition(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Position", EditorStyles.boldLabel);
        auxArg.displayPosition = EditorGUILayout.Toggle(auxArg.displayPosition);
        EditorGUILayout.EndHorizontal();

        if (auxArg.displayPosition)
        {
            var posLocal = animationController.transform.localPosition;
            EditorGUILayout.BeginHorizontal();
            GUI.color = Color.yellow;
            EditorGUILayout.HelpBox($"local Position ={posLocal.x},{posLocal.y},{posLocal.z} ", MessageType.None);
            if (GUILayout.Button("[As target]"))
                auxArg.targetPosition = posLocal;
            EditorGUILayout.EndHorizontal();
            GUI.color = Color.white;
            auxArg.targetPosition = EditorGUILayout.Vector3Field("Target Position", auxArg.targetPosition);
            auxArg.atractor = (Transform)EditorGUILayout.ObjectField("Atractor", auxArg.atractor, typeof(Transform));

            if (auxArg.atractor)
                EditorGUILayout.HelpBox("The attractor uses local position, so make sure it stays within the same environment or a similar one.", MessageType.Info);
        }

        EditorGUILayout.EndVertical();//---- quadPos
    }

    protected override void ShowTargetScale(AnimationAssistant auxArg)
    {

        EditorGUILayout.BeginVertical("box"); //----quad position
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
        auxArg.displayScale = EditorGUILayout.Toggle(auxArg.displayScale);
        EditorGUILayout.EndHorizontal();

        if (auxArg.displayScale)
            auxArg.targetScale = EditorGUILayout.Vector3Field("Target Scale", auxArg.targetScale);

        EditorGUILayout.EndVertical();//---- quadPos
    }

    protected override void ShowTargetRotation(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Rotation", EditorStyles.boldLabel);
        auxArg.displayRotation = EditorGUILayout.Toggle(auxArg.displayRotation);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayRotation)
        {
            auxArg.rotationType = (RotateMode)EditorGUILayout.EnumPopup("Rotation type", auxArg.rotationType);
            auxArg.targetRotation = EditorGUILayout.Vector3Field("Target Rotation", auxArg.targetRotation);
        }
        EditorGUILayout.EndVertical();//---- quadPos

    }
#if ANIMA_DOTWEEN_PRO
    protected  void ShowColor(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Color", EditorStyles.boldLabel);
        auxArg.displayColor = EditorGUILayout.Toggle(auxArg.displayColor);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayColor)
        {
            auxArg.colorTarget = EditorGUILayout.ColorField("Color Target", auxArg.colorTarget);
        }
        EditorGUILayout.EndVertical();
    }
#endif

    private void ShowSizeDelta(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("SizeDelta", EditorStyles.boldLabel);
        auxArg.displaySizeDelta = EditorGUILayout.Toggle(auxArg.displaySizeDelta);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displaySizeDelta)
            auxArg.targetSizeDelta = EditorGUILayout.Vector2Field("Target Size", auxArg.targetSizeDelta);
        EditorGUILayout.EndVertical();//---- quadPos
    }


    private void ShowFade(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Fade", EditorStyles.boldLabel);
        auxArg.displayFade = EditorGUILayout.Toggle(auxArg.displayFade);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayFade)
        {
            auxArg.fadeTarget = EditorGUILayout.FloatField("Fade value", auxArg.fadeTarget);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Apply on CanvasGroup");
            auxArg.applyOnCanvasGroup = EditorGUILayout.Toggle(auxArg.applyOnCanvasGroup);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();//---- quadPos
    }

#if ANIMA_DOTWEEN_PRO
    private void ShowColorOutline(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Color Outline", EditorStyles.boldLabel);
        auxArg.displayTextOutlineColor = EditorGUILayout.Toggle(auxArg.displayTextOutlineColor);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayTextOutlineColor)
        {
            auxArg.colorTarget = EditorGUILayout.ColorField("Color Target", auxArg.colorTarget);
        }
        EditorGUILayout.EndVertical();//---- quadPos
    }
#endif

    private void ShowCharacterSplit(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Character Separation", EditorStyles.boldLabel);
        auxArg.displayTextCharaterSplit = EditorGUILayout.Toggle(auxArg.displayTextCharaterSplit);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayTextCharaterSplit)
        {
            auxArg.charSplittTarget = EditorGUILayout.FloatField("Separation value", auxArg.charSplittTarget);
        }
        EditorGUILayout.EndVertical();//---- quadPos
    }

    private void ShowWordSplit(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Word Separation", EditorStyles.boldLabel);
        auxArg.displayTextWordSplit = EditorGUILayout.Toggle(auxArg.displayTextWordSplit);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayTextWordSplit)
        {
            auxArg.wordSplitTarget = EditorGUILayout.FloatField("Separation value", auxArg.wordSplitTarget);
        }
        EditorGUILayout.EndVertical();//---- quadPos
    }

    private void ShowLineSeparation(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Line Separation", EditorStyles.boldLabel);
        auxArg.displayTextLineSplit = EditorGUILayout.Toggle(auxArg.displayTextLineSplit);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayTextLineSplit)
        {
            auxArg.lineSplitTarget = EditorGUILayout.FloatField("Separation value", auxArg.lineSplitTarget);
        }
        EditorGUILayout.EndVertical();//---- quadPos
    }

#if ANIMA_DOTWEEN_PRO
    private void ShowNewText(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Text", EditorStyles.boldLabel);
        auxArg.displayTextChange = EditorGUILayout.Toggle(auxArg.displayTextChange);
        EditorGUILayout.EndHorizontal();

        if (auxArg.displayTextChange)
        {
            EditorGUILayout.LabelField("New Text", EditorStyles.label);
            auxArg.newText = EditorGUILayout.TextArea(
                auxArg.newText,
                GUILayout.Height(60)
            );
        }

        EditorGUILayout.EndVertical();
    }
#endif
}
#endif