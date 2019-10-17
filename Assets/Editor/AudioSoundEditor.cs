using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(Audio), true)]
public class AudioSoundEditor : Editor {
    [SerializeField] private AudioSource sourcePreviewer;

    private void OnEnable() {

    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        sourcePreviewer = EditorUtility
            .CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource))
            .GetComponent<AudioSource>();

        if (GUILayout.Button("Preview")) {
            ((Audio) target).Play();
            PublicAudioUtil.PlayClip(sourcePreviewer.clip);
        }
    }
}
