using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Audio), true)]
public class AudioEditor : Editor
{
    private AudioSource customSource;

    public void OnEnable()
    {
        customSource = EditorUtility.CreateGameObjectWithHideFlags("Preview", HideFlags.HideAndDontSave,
            typeof(AudioSource)).GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        DestroyImmediate(customSource.gameObject);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Separator();
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Play"))
        {
            ((Audio) target).Play(customSource);
        }

        if (GUILayout.Button("Stop"))
        {
            ((Audio) target).Stop(customSource);
        }
    }
}
