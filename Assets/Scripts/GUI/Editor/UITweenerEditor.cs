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

        private void OnEnable() {
            fadeInDurationProperty = serializedObject.FindProperty("fadeData.fadeInDuration");
            
        }

        public override void OnInspectorGUI()
        {

            serializedObject.Update();

            UITweenerCustomEditor = (UITweener) target;
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

                if (UITweenerCustomEditor.UseDefaultPosition) {
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

                UITweenerCustomEditor.ScaleFrom = EditorGUILayout.Vector3Field("Scale From", UITweenerCustomEditor.ScaleFrom);

                UITweenerCustomEditor.ScaleTo = EditorGUILayout.Vector3Field("Scale to", UITweenerCustomEditor.ScaleTo);
                
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

        Vector3 positionHold;
        protected virtual void ShowAnimationIn(UITweener customWindowEditor)
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Animation In", EditorStyles.boldLabel);
            customWindowEditor.AnimInType = (AnimationType)EditorGUILayout.EnumPopup("Animation In Type", customWindowEditor.AnimInType);
            GUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("GroupBox");


            if (customWindowEditor.AnimInType == AnimationType.NONE)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("You should pick an animation");
                EditorGUILayout.EndVertical();
            }

            if (customWindowEditor.AnimInType == AnimationType.FADE) {
                EditorGUILayout.PropertyField(fadeInDurationProperty);
                //customWindowEditor.FadeInDuration = EditorGUILayout.FloatField("Fade In Duration", customWindowEditor.FadeInDuration);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Fade to");
                customWindowEditor.FadeTo = EditorGUILayout.Slider(customWindowEditor.FadeTo, 0, 1);
                EditorGUILayout.EndHorizontal();
            }

            if (customWindowEditor.AnimInType == AnimationType.MOVEMENT)
            {
                customWindowEditor.MoveInDuration = EditorGUILayout.FloatField("Move In Duration", customWindowEditor.MoveInDuration);

                customWindowEditor.MoveEaseInType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease In Type", customWindowEditor.MoveEaseInType);

                customWindowEditor.UseDefaultPosition = EditorGUILayout.Toggle("Use Own Position: ", customWindowEditor.UseDefaultPosition);



                customWindowEditor.MoveInFrom = EditorGUILayout.Vector3Field("Move From", customWindowEditor.MoveInFrom);

                if (customWindowEditor.UseDefaultPosition) {
                    customWindowEditor.MoveOriginal = customWindowEditor.originalPosition;
                }

                customWindowEditor.MoveOriginal = EditorGUILayout.Vector3Field("Move to", customWindowEditor.MoveOriginal);



            }

            if (customWindowEditor.AnimInType == AnimationType.SCALE)
            {
                customWindowEditor.ScaleInDuration = EditorGUILayout.FloatField("Scale In Duration", customWindowEditor.ScaleInDuration);

                customWindowEditor.ScaleEaseInType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease In Type", customWindowEditor.ScaleEaseInType);

                customWindowEditor.ScaleFrom = EditorGUILayout.Vector3Field("Scale From", customWindowEditor.ScaleFrom);

                customWindowEditor.ScaleTo = EditorGUILayout.Vector3Field("Scale to", customWindowEditor.ScaleTo);
            }
        
            if (customWindowEditor.AnimInType == AnimationType.ROTATE)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("You cant init a transition with only rotation.");
                customWindowEditor.RotateInDuration = EditorGUILayout.FloatField("TODO: Rotate In Duration", customWindowEditor.RotateInDuration);
                EditorGUILayout.EndVertical();

            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        protected virtual void ShowAnimationOut(UITweener customWindowEditor)
        {


            EditorGUILayout.Separator();

            GUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Animation Out", EditorStyles.boldLabel);
            customWindowEditor.AnimOutType = (AnimationType)EditorGUILayout.EnumPopup("Animation Out Type", customWindowEditor.AnimOutType);

            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical("GroupBox");

            if (customWindowEditor.AnimOutType == AnimationType.NONE)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("You should pick an animation");
                EditorGUILayout.EndVertical();
            }

            if (customWindowEditor.AnimOutType == AnimationType.FADE)
            {

                customWindowEditor.FadeOutDuration = EditorGUILayout.FloatField("Fade Out Duration", customWindowEditor.FadeOutDuration);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Fade to");
                customWindowEditor.FadeFrom = EditorGUILayout.Slider(customWindowEditor.FadeFrom, 0, 1);
                EditorGUILayout.EndHorizontal();
            }

            if (customWindowEditor.AnimOutType == AnimationType.MOVEMENT)
            {
                customWindowEditor.MoveOutDuration = EditorGUILayout.FloatField("Move Out Duration", customWindowEditor.MoveOutDuration);

                customWindowEditor.MoveEaseOutType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease Out Type", customWindowEditor.MoveEaseOutType);

                customWindowEditor.UseDefaultPosition = EditorGUILayout.Toggle("Use Own Position: ", customWindowEditor.UseDefaultPosition);

                customWindowEditor.MoveOriginal = EditorGUILayout.Vector3Field("Move From", customWindowEditor.MoveOriginal);
                
                customWindowEditor.MoveOutTo = EditorGUILayout.Vector3Field("Move To", customWindowEditor.MoveOutTo);
                
                if (customWindowEditor.UseDefaultPosition)
                {
                    customWindowEditor.MoveOriginal = customWindowEditor.originalPosition;
                }
            }

            if (customWindowEditor.AnimOutType == AnimationType.SCALE)
            {
                customWindowEditor.ScaleOutDuration = EditorGUILayout.FloatField("Scale Out Duration", customWindowEditor.ScaleOutDuration);

                customWindowEditor.ScaleEaseOutType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease Out Type", customWindowEditor.ScaleEaseOutType);

                customWindowEditor.ScaleFrom = EditorGUILayout.Vector3Field("Scale From", customWindowEditor.ScaleFrom);

                customWindowEditor.ScaleTo = EditorGUILayout.Vector3Field("Scale to", customWindowEditor.ScaleTo);
            }
        
            if (customWindowEditor.AnimOutType == AnimationType.ROTATE)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("You cannot end a transition with rotate.");
                //customWindowEditor.RotateOutDuration = EditorGUILayout.FloatField("Rotate Out Duration", customWindowEditor.RotateOutDuration);

                EditorGUILayout.LabelField("TODO: Rotate Out Duration");
                EditorGUILayout.EndVertical();
                
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();


        }


    }
