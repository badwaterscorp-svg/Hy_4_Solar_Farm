using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;


#if UNITY_EDITOR
[CustomEditor(typeof(AnimationUIController))]
public class EditorDoUIAnimator : BaseEditorAnimator
{
    //protected new AnimationUIController animationController;
    public override void OnInspectorGUI()
    {
        animationController = (AnimationUIController)target;
        base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(animationController);
        }
    }

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
        ShowTargetSprite(animationAux);
        ShowTargetRotation(animationAux);
        ShowColor(animationAux);
        ShowSizeDelta(animationAux);
        ShowPixelPerUnitMultiplier(animationAux);
        ShowFade(animationAux);
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
            if (GUILayout.Button("[As Position]"))
                animationController.transform.localPosition = auxArg.targetPosition;
            EditorGUILayout.EndHorizontal();
            GUI.color = Color.white;
            auxArg.targetPosition = EditorGUILayout.Vector3Field("Target Position", auxArg.targetPosition);
            auxArg.atractor = (Transform)EditorGUILayout.ObjectField("Atractor", auxArg.atractor, typeof(Transform));

            if(auxArg.atractor)
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


    protected void ShowColor(AnimationAssistant auxArg)
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

    private void ShowTargetSprite(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box"); //----quad position
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sprite Change", EditorStyles.boldLabel);
        auxArg.displayTexture = EditorGUILayout.Toggle(auxArg.displayTexture);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayTexture)
        {
            SerializedProperty objSer;
            objSer = serializedObject.FindProperty("listAux");
            SerializedProperty spriteShift;
            spriteShift = objSer.GetArrayElementAtIndex(animationController.listAux.IndexOf(auxArg));
            EditorGUILayout.PropertyField(spriteShift.FindPropertyRelative("spriteShift"), new GUIContent("SpriteShift"));
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.EndVertical();//---- quadPos
    }

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

    private void ShowPixelPerUnitMultiplier(AnimationAssistant auxArg)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Pixel Perfect", EditorStyles.boldLabel);
        auxArg.displayPixelMultiplier = EditorGUILayout.Toggle(auxArg.displayPixelMultiplier);
        EditorGUILayout.EndHorizontal();
        if (auxArg.displayPixelMultiplier)
            auxArg.pixelMultiplier = EditorGUILayout.FloatField("Sliced Multiplier", auxArg.pixelMultiplier);
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
}

#endif