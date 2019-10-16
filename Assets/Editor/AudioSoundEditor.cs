using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Audio), true)]
public class AudioSoundEditor : Editor {
    [SerializeField] private AudioSource sourcePreviewer;

    private void OnEnable() {
        sourcePreviewer = EditorUtility
            .CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource))
            .GetComponent<AudioSource>();
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview")) {
            ((Audio) target).Play(sourcePreviewer);
        }
        EditorGUI.EndDisabledGroup();
    }
}
