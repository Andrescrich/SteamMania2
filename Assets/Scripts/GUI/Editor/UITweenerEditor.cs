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
    
    #region SerializedProperties

    private SerializedProperty startHiddenProperty;
    
    private SerializedProperty useFadeInProperty;
    private SerializedProperty useMoveInProperty;
    private SerializedProperty useScaleInProperty;
    private SerializedProperty useRotateInProperty;

    private SerializedProperty useFadeOutProperty;
    private SerializedProperty useMoveOutProperty;
    private SerializedProperty useScaleOutProperty;
    private SerializedProperty useRotateOutProperty;
    
    private SerializedProperty fadeInDurationProperty;
    private SerializedProperty fadeOutDurationProperty;
    private SerializedProperty fadeFromProperty, fadeToProperty;

    private SerializedProperty moveInDurationProperty;
    private SerializedProperty moveOutDurationProperty;
    private SerializedProperty moveEaseInTypeProperty;
    private SerializedProperty moveEaseOutTypeProperty;
    private SerializedProperty useDefaultPositionProperty;
    private SerializedProperty moveInFromProperty, moveOriginalProperty, moveOutProperty;

    private SerializedProperty scaleInDurationProperty;
    private SerializedProperty scaleOutDurationProperty;
    private SerializedProperty scaleInEaseTypeProperty;
    private SerializedProperty scaleOutEaseTypeProperty;
    private SerializedProperty scaleFromProperty, scaleToProperty;

    private SerializedProperty rotationInDurationProperty;
    private SerializedProperty rotationOutDurationProperty;
    private SerializedProperty rotationEaseInProperty;
    private SerializedProperty rotationEaseOutProperty;
    private SerializedProperty rotateFromProperty, rotateToProperty;

    #endregion

    private void OnEnable()
    {    
        startHiddenProperty = serializedObject.FindProperty("startHidden");
        
        useFadeInProperty = serializedObject.FindProperty("useFadeIn");
        useMoveInProperty = serializedObject.FindProperty("useMovementIn");
        useScaleInProperty = serializedObject.FindProperty("useScaleIn");
        useRotateInProperty = serializedObject.FindProperty("useRotateIn");
        
        useFadeOutProperty = serializedObject.FindProperty("useFadeOut");
        useMoveOutProperty = serializedObject.FindProperty("useMovementOut");
        useScaleOutProperty = serializedObject.FindProperty("useScaleOut");
        useRotateOutProperty = serializedObject.FindProperty("useRotateOut");

        fadeInDurationProperty = serializedObject.FindProperty("fadeData.fadeInDuration");
        fadeOutDurationProperty = serializedObject.FindProperty("fadeData.fadeOutDuration");
        fadeFromProperty = serializedObject.FindProperty("fadeData.fadeFrom");
        fadeToProperty = serializedObject.FindProperty("fadeData.fadeTo");
        
        moveInDurationProperty = serializedObject.FindProperty("moveData.moveInDuration");
        moveOutDurationProperty = serializedObject.FindProperty("moveData.moveOutDuration");
        moveEaseInTypeProperty = serializedObject.FindProperty("moveData.moveEaseInType");
        moveEaseOutTypeProperty = serializedObject.FindProperty("moveData.moveEaseOutType");
        useDefaultPositionProperty = serializedObject.FindProperty("moveData.useDefaultPosition");
        moveInFromProperty = serializedObject.FindProperty("moveData.moveInFrom");
        moveOriginalProperty = serializedObject.FindProperty("moveData.moveOriginal");
        moveOutProperty = serializedObject.FindProperty("moveData.moveOutTo");
        
        scaleInDurationProperty = serializedObject.FindProperty("scaleData.scaleInDuration");
        scaleOutDurationProperty = serializedObject.FindProperty("scaleData.scaleOutDuration");
        scaleInEaseTypeProperty = serializedObject.FindProperty("scaleData.scaleEaseInType");
        scaleOutEaseTypeProperty = serializedObject.FindProperty("scaleData.scaleEaseOutType");
        scaleFromProperty = serializedObject.FindProperty("scaleData.scaleFrom");
        scaleToProperty = serializedObject.FindProperty("scaleData.scaleTo");
        
        rotationInDurationProperty = serializedObject.FindProperty("rotateData.rotateInDuration");
        rotationOutDurationProperty = serializedObject.FindProperty("scaleData.rotateOutDuration");
        rotationEaseInProperty = serializedObject.FindProperty("scaleData.rotateEaseInType");
        rotationEaseOutProperty = serializedObject.FindProperty("scaleData.rotateEaseOutType");
        rotateFromProperty = serializedObject.FindProperty("scaleData.rotateFrom");
        rotateToProperty = serializedObject.FindProperty("scaleData.rotateTo");
        

    }

    
    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        UITweenerCustomEditor = (UITweener)target;
        
        startHiddenProperty.boolValue = EditorGUIExtension.ToggleButton(startHiddenProperty.boolValue, "Start Hidden");
        //UITweenerCustomEditor.StartHidden = EditorGUILayout.Toggle("Start Hidden", UITweenerCustomEditor.StartHidden);

        
        EditorGUILayout.LabelField("Animation In", EditorStyles.boldLabel);

        useFadeInProperty.boolValue = EditorGUIExtension.ToggleButton(useFadeInProperty.boolValue, "Use Fade In");


        if (useFadeInProperty.boolValue)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Fade", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("GroupBox");
            
                EditorGUILayout.PropertyField(fadeInDurationProperty);
                fadeInDurationProperty.floatValue = Mathf.Clamp(fadeInDurationProperty.floatValue, 0.1f, 5f);
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Slider(fadeToProperty, 0, 1, "Fade Out");
                EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        //UITweenerCustomEditor.useMovementIn = EditorGUILayout.Toggle("Use Movement", UITweenerCustomEditor.useMovementIn);
        useMoveInProperty.boolValue = EditorGUIExtension.ToggleButton(useMoveInProperty.boolValue, "Use Movement In");
        
        
        if (useMoveInProperty.boolValue)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("GroupBox");
            
                EditorGUILayout.PropertyField(moveInDurationProperty);
                
                moveInDurationProperty.floatValue = Mathf.Clamp(moveInDurationProperty.floatValue, 0.1f, 5f);

                EditorGUILayout.PropertyField(moveEaseInTypeProperty);

                EditorGUILayout.PropertyField(useDefaultPositionProperty);

                EditorGUILayout.PropertyField(moveInFromProperty);

                if (useDefaultPositionProperty.boolValue)
                {
                    moveOriginalProperty.vector3Value = UITweenerCustomEditor.originalPosition;
                }

                EditorGUILayout.PropertyField(moveOriginalProperty);

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        
        useScaleInProperty.boolValue = EditorGUIExtension.ToggleButton(useScaleInProperty.boolValue, "Use Scale In");
        

        if (useScaleInProperty.boolValue)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("GroupBox");
            
                EditorGUILayout.BeginVertical();

                EditorGUILayout.PropertyField(scaleInDurationProperty);

                scaleInDurationProperty.floatValue = Mathf.Clamp(scaleInDurationProperty.floatValue, 0.1f, 5f);

                EditorGUILayout.PropertyField(scaleInEaseTypeProperty);

                EditorGUILayout.PropertyField(scaleFromProperty);
                
                EditorGUILayout.PropertyField(scaleToProperty);

            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }


        useRotateInProperty.boolValue = EditorGUIExtension.ToggleButton(useRotateInProperty.boolValue, "Use Rotate In");

        if (useRotateInProperty.boolValue)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Rotate", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("GroupBox");
                
                EditorGUILayout.BeginVertical();

                EditorGUILayout.PropertyField(rotationInDurationProperty);
                
                rotationInDurationProperty.floatValue = Mathf.Clamp(rotationInDurationProperty.floatValue, 0.1f, 5f);

            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        
        
        
        
        EditorGUILayout.Separator();
        
        
        
        EditorGUILayout.LabelField("Animation Out", EditorStyles.boldLabel);

        
        
        
        useFadeOutProperty.boolValue = EditorGUIExtension.ToggleButton(useFadeOutProperty.boolValue, "Use Fade Out");
        
        if (useFadeOutProperty.boolValue)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Fade", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("GroupBox");
            
                EditorGUILayout.PropertyField(fadeOutDurationProperty);
                fadeOutDurationProperty.floatValue = Mathf.Clamp(fadeOutDurationProperty.floatValue, 0.1f, 5f);
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Slider(fadeToProperty, 0, 1, "Fade To");
                EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        //UITweenerCustomEditor.useMovementIn = EditorGUILayout.Toggle("Use Movement", UITweenerCustomEditor.useMovementIn);
        useMoveOutProperty.boolValue = EditorGUIExtension.ToggleButton(useMoveOutProperty.boolValue, "Use Movement Out");
        
        
        if (useMoveOutProperty.boolValue)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("GroupBox");
            
                EditorGUILayout.PropertyField(moveOutDurationProperty);
                
                moveOutDurationProperty.floatValue = Mathf.Clamp(moveOutDurationProperty.floatValue, 0.1f, 5f);

                EditorGUILayout.PropertyField(moveEaseOutTypeProperty);

                EditorGUILayout.PropertyField(useDefaultPositionProperty);

                if (useDefaultPositionProperty.boolValue)
                {
                    moveOriginalProperty.vector3Value = UITweenerCustomEditor.originalPosition;
                }

                EditorGUILayout.PropertyField(moveOriginalProperty);
                
                EditorGUILayout.PropertyField(moveOutProperty);


            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        
        useScaleOutProperty.boolValue = EditorGUIExtension.ToggleButton(useScaleOutProperty.boolValue, "Use Scale Out");
        

        if (useScaleOutProperty.boolValue)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("GroupBox");
            
                EditorGUILayout.BeginVertical();

                EditorGUILayout.PropertyField(scaleOutDurationProperty);

                scaleOutDurationProperty.floatValue = Mathf.Clamp(scaleOutDurationProperty.floatValue, 0.1f, 5f);

                EditorGUILayout.PropertyField(scaleOutEaseTypeProperty);

                EditorGUILayout.PropertyField(scaleToProperty);
                
                EditorGUILayout.PropertyField(scaleFromProperty);
                

            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }


        useRotateOutProperty.boolValue = EditorGUIExtension.ToggleButton(useRotateOutProperty.boolValue, "Use Rotate Out");

        if (useRotateOutProperty.boolValue)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Rotate", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("GroupBox");
                
                EditorGUILayout.BeginVertical();

                EditorGUILayout.PropertyField(rotationOutDurationProperty);
                
                rotationOutDurationProperty.floatValue = Mathf.Clamp(rotationOutDurationProperty.floatValue, 0.1f, 5f);

            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }


        
        
        
        
        if (GUI.changed)
        {
           
        }
        serializedObject.ApplyModifiedProperties();
        
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

}
