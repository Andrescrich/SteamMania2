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

    private float LimitValue(float value, float min, float max)
    {
        if (value <= min)
        {
            value = min;
        }

        if (value >= max)
        {
            value = max;
        }
        return value;
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        UITweenerCustomEditor = (UITweener)target;
        
        UITweenerCustomEditor.StartHidden = EditorGUIExtension.ToggleButton(UITweenerCustomEditor.StartHidden, "Start Hidden");
        //UITweenerCustomEditor.StartHidden = EditorGUILayout.Toggle("Start Hidden", UITweenerCustomEditor.StartHidden);

        EditorGUILayout.LabelField("Animation In", EditorStyles.boldLabel);

        //UITweenerCustomEditor.useFadeIn = EditorGUILayout.Toggle("Use Fade", UITweenerCustomEditor.useFadeIn);
        UITweenerCustomEditor.useFadeIn = EditorGUIExtension.ToggleButton(UITweenerCustomEditor.useFadeIn, "Use Fade In");


        if (UITweenerCustomEditor.useFadeIn)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Fade", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            //EditorGUILayout.PropertyField(fadeInDurationProperty);
            UITweenerCustomEditor.FadeInDuration = EditorGUILayout.FloatField("Fade In Duration", UITweenerCustomEditor.FadeInDuration);
            UITweenerCustomEditor.FadeInDuration = LimitValue(UITweenerCustomEditor.FadeInDuration, 0.1f, 5f);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Fade to");
            UITweenerCustomEditor.FadeTo = EditorGUILayout.Slider(UITweenerCustomEditor.FadeTo, 0, 1);
            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        //UITweenerCustomEditor.useMovementIn = EditorGUILayout.Toggle("Use Movement", UITweenerCustomEditor.useMovementIn);
        UITweenerCustomEditor.useMovementIn = EditorGUIExtension.ToggleButton(UITweenerCustomEditor.useMovementIn, "Use Movement In");

        if (UITweenerCustomEditor.useMovementIn)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            UITweenerCustomEditor.MoveInDuration = EditorGUILayout.FloatField("Move In Duration", UITweenerCustomEditor.MoveInDuration);
            UITweenerCustomEditor.MoveInDuration = LimitValue(UITweenerCustomEditor.MoveInDuration, 0.1f, 5f);
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

        //UITweenerCustomEditor.useScaleIn = EditorGUILayout.Toggle("Use Scale", UITweenerCustomEditor.useScaleIn);
        UITweenerCustomEditor.useScaleIn = EditorGUIExtension.ToggleButton(UITweenerCustomEditor.useScaleIn, "Use Scale In");


        if (UITweenerCustomEditor.useScaleIn)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginVertical();
            UITweenerCustomEditor.ScaleInDuration = EditorGUILayout.FloatField("Scale In Duration", UITweenerCustomEditor.ScaleInDuration);
            UITweenerCustomEditor.ScaleInDuration = LimitValue(UITweenerCustomEditor.ScaleInDuration, 0.1f, 5f);
            UITweenerCustomEditor.ScaleEaseInType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease In Type", UITweenerCustomEditor.ScaleEaseInType);

            UITweenerCustomEditor.ScaleFrom = EditorGUILayout.Vector3Field("Scale From", UITweenerCustomEditor.ScaleFrom);

            UITweenerCustomEditor.ScaleTo = EditorGUILayout.Vector3Field("Scale to", UITweenerCustomEditor.ScaleTo);
            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }


        //UITweenerCustomEditor.useRotateIn = EditorGUILayout.Toggle("Use Rotate", UITweenerCustomEditor.useRotateIn);
        UITweenerCustomEditor.useRotateIn = EditorGUIExtension.ToggleButton(UITweenerCustomEditor.useRotateIn, "Use Rotate In");
        if (UITweenerCustomEditor.useRotateIn)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Rotate", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginVertical();
            UITweenerCustomEditor.RotateInDuration = EditorGUILayout.FloatField("Rotate In Duration", UITweenerCustomEditor.RotateInDuration);
            UITweenerCustomEditor.RotateInDuration = LimitValue(UITweenerCustomEditor.RotateInDuration, 0.1f, 5f);
            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }










        EditorGUILayout.LabelField("Animation Out", EditorStyles.boldLabel);

        //UITweenerCustomEditor.useFadeOut = EditorGUILayout.Toggle("Use Fade", UITweenerCustomEditor.useFadeOut);
        
        UITweenerCustomEditor.useFadeOut = EditorGUIExtension.ToggleButton(UITweenerCustomEditor.useFadeOut, "Use Fade Out");


        if (UITweenerCustomEditor.useFadeOut)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Fade", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            UITweenerCustomEditor.FadeOutDuration = EditorGUILayout.FloatField("Fade Out Duration", UITweenerCustomEditor.FadeOutDuration);
            UITweenerCustomEditor.FadeOutDuration = LimitValue(UITweenerCustomEditor.FadeOutDuration, 0.1f, 5f);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Fade to");
            UITweenerCustomEditor.FadeFrom = EditorGUILayout.Slider(UITweenerCustomEditor.FadeFrom, 0, 1);
            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        
        //UITweenerCustomEditor.useMovementOut = EditorGUILayout.Toggle("Use Movement", UITweenerCustomEditor.useMovementOut);
        UITweenerCustomEditor.useMovementOut = EditorGUIExtension.ToggleButton(UITweenerCustomEditor.useMovementOut, "Use Movement Out");

        if (UITweenerCustomEditor.useMovementOut)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            UITweenerCustomEditor.MoveOutDuration = EditorGUILayout.FloatField("Move Out Duration", UITweenerCustomEditor.MoveOutDuration);
            UITweenerCustomEditor.MoveOutDuration = LimitValue(UITweenerCustomEditor.MoveOutDuration, 0.1f, 5f);
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

        //UITweenerCustomEditor.useScaleOut = EditorGUILayout.Toggle("Use Scale", UITweenerCustomEditor.useScaleOut);
        UITweenerCustomEditor.useScaleOut = EditorGUIExtension.ToggleButton(UITweenerCustomEditor.useScaleOut, "Use Scale Out");

        if (UITweenerCustomEditor.useScaleOut)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginVertical();

            UITweenerCustomEditor.ScaleOutDuration = EditorGUILayout.FloatField("Scale Out Duration", UITweenerCustomEditor.ScaleOutDuration);
            UITweenerCustomEditor.ScaleOutDuration = LimitValue(UITweenerCustomEditor.ScaleOutDuration, 0.1f, 5f);
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
