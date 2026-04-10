using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

#if UNITY_EDITOR

[CustomEditor(typeof(AnimationController))]
public class EditorDoAnimator : BaseEditorAnimator
{
    public override void OnInspectorGUI()
    {
        animationController = (AnimationController)target;
        base.OnInspectorGUI();
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
        ShowTargetRotation(animationAux);
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
            auxArg.targetRotation = EditorGUILayout.Vector3Field("Target Rotation", auxArg.targetRotation);
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


}

#endif
