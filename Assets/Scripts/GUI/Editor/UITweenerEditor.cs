using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(UITweener), true)]
[CanEditMultipleObjects]
public class UIComponentEditor : Editor
{
    private bool animationInEnabled = true;
    private bool animationOutEnabled = true;

    protected UITweener UITweenerCustomEditor;
    private SerializedProperty fadeInDurationProperty;

    private void OnEnable()
    {
        fadeInDurationProperty = serializedObject.FindProperty("fadeData.fadeInDuration");

    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        UITweenerCustomEditor = (UITweener)target;
        UITweenerCustomEditor.StartHidden = EditorGUILayout.Toggle("Start Hidden", UITweenerCustomEditor.StartHidden);

        EditorGUILayout.LabelField("Animation In", EditorStyles.boldLabel);

        UITweenerCustomEditor.useFadeIn = EditorGUILayout.Toggle("Use Fade", UITweenerCustomEditor.useFadeIn);


        if (UITweenerCustomEditor.useFadeIn)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Fade", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.PropertyField(fadeInDurationProperty);
            //customWindowEditor.FadeInDuration = EditorGUILayout.FloatField("Fade In Duration", customWindowEditor.FadeInDuration);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Fade to");
            UITweenerCustomEditor.FadeTo = EditorGUILayout.Slider(UITweenerCustomEditor.FadeTo, 0, 1);
            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        UITweenerCustomEditor.useMovementIn = EditorGUILayout.Toggle("Use Movement", UITweenerCustomEditor.useMovementIn);

        if (UITweenerCustomEditor.useMovementIn)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            UITweenerCustomEditor.MoveInDuration = EditorGUILayout.FloatField("Move In Duration", UITweenerCustomEditor.MoveInDuration);

            UITweenerCustomEditor.MoveEaseInType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease In Type", UITweenerCustomEditor.MoveEaseInType);

            UITweenerCustomEditor.UseDefaultPosition = EditorGUILayout.Toggle("Use Own Position: ", UITweenerCustomEditor.UseDefaultPosition);



            UITweenerCustomEditor.MoveInFrom = EditorGUILayout.Vector3Field("Move From", UITweenerCustomEditor.MoveInFrom);

            if (UITweenerCustomEditor.UseDefaultPosition)
            {
                UITweenerCustomEditor.MoveOriginal = UITweenerCustomEditor.originalPosition;
            }

            UITweenerCustomEditor.MoveOriginal = EditorGUILayout.Vector3Field("Move to", UITweenerCustomEditor.MoveOriginal);


            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        UITweenerCustomEditor.useScaleIn = EditorGUILayout.Toggle("Use Scale", UITweenerCustomEditor.useScaleIn);

        if (UITweenerCustomEditor.useScaleIn)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginVertical();
            UITweenerCustomEditor.ScaleInDuration = EditorGUILayout.FloatField("Scale In Duration", UITweenerCustomEditor.ScaleInDuration);

            UITweenerCustomEditor.ScaleEaseInType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease In Type", UITweenerCustomEditor.ScaleEaseInType);

            UITweenerCustomEditor.ScaleFrom = EditorGUILayout.Vector3Field("Scale From", UITweenerCustomEditor.ScaleFrom);

            UITweenerCustomEditor.ScaleTo = EditorGUILayout.Vector3Field("Scale to", UITweenerCustomEditor.ScaleTo);
            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }


        UITweenerCustomEditor.useRotateIn = EditorGUILayout.Toggle("Use Rotate", UITweenerCustomEditor.useRotateIn);

        if (UITweenerCustomEditor.useRotateIn)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Rotate", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginVertical();
            UITweenerCustomEditor.RotateInDuration = EditorGUILayout.FloatField("Rotate In Duration", UITweenerCustomEditor.RotateInDuration);

            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }










        EditorGUILayout.LabelField("Animation Out", EditorStyles.boldLabel);

        UITweenerCustomEditor.useFadeOut = EditorGUILayout.Toggle("Use Fade", UITweenerCustomEditor.useFadeOut);


        if (UITweenerCustomEditor.useFadeOut)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Fade", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            UITweenerCustomEditor.FadeOutDuration = EditorGUILayout.FloatField("Fade Out Duration", UITweenerCustomEditor.FadeOutDuration);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Fade to");
            UITweenerCustomEditor.FadeFrom = EditorGUILayout.Slider(UITweenerCustomEditor.FadeFrom, 0, 1);
            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        UITweenerCustomEditor.useMovementOut = EditorGUILayout.Toggle("Use Movement", UITweenerCustomEditor.useMovementOut);

        if (UITweenerCustomEditor.useMovementOut)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            UITweenerCustomEditor.MoveOutDuration = EditorGUILayout.FloatField("Move Out Duration", UITweenerCustomEditor.MoveOutDuration);

            UITweenerCustomEditor.MoveEaseOutType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease Out Type", UITweenerCustomEditor.MoveEaseOutType);

            UITweenerCustomEditor.UseDefaultPosition = EditorGUILayout.Toggle("Use Own Position: ", UITweenerCustomEditor.UseDefaultPosition);

            UITweenerCustomEditor.MoveOriginal = EditorGUILayout.Vector3Field("Move From", UITweenerCustomEditor.MoveOriginal);

            UITweenerCustomEditor.MoveOutTo = EditorGUILayout.Vector3Field("Move To", UITweenerCustomEditor.MoveOutTo);

            if (UITweenerCustomEditor.UseDefaultPosition)
            {
                UITweenerCustomEditor.MoveOriginal = UITweenerCustomEditor.originalPosition;
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        UITweenerCustomEditor.useScaleOut = EditorGUILayout.Toggle("Use Scale", UITweenerCustomEditor.useScaleOut);

        if (UITweenerCustomEditor.useScaleOut)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginVertical();

            UITweenerCustomEditor.ScaleOutDuration = EditorGUILayout.FloatField("Scale Out Duration", UITweenerCustomEditor.ScaleOutDuration);

            UITweenerCustomEditor.ScaleEaseOutType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease Out Type", UITweenerCustomEditor.ScaleEaseOutType);

            UITweenerCustomEditor.ScaleTo = EditorGUILayout.Vector3Field("Scale From", UITweenerCustomEditor.ScaleTo);

            UITweenerCustomEditor.ScaleFrom = EditorGUILayout.Vector3Field("Scale To", UITweenerCustomEditor.ScaleFrom);

            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

}
