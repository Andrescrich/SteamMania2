using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Audio), true)]
    public class AudioSoundEditor : UnityEditor.Editor {
        [SerializeField] private AudioSource sourcePreviewer;
        private void OnEnable()
        {
            GameObject go = GameObject.Find("AudioManager");
        
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            Audio audioEditor = (Audio) target;
            sourcePreviewer = EditorUtility
                .CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource))
                .GetComponent<AudioSource>();

            if (GUILayout.Button("Preview")) {
                audioEditor.Play(sourcePreviewer);
            }
        }
    }
}
