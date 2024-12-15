#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UnityEditor.Audio
{
    [CustomPropertyDrawer(typeof(AudioClip))]
    public class AudioClipDrawer : PropertyDrawer
    {
        private enum PlaybackState { Play, Stop }
        private static readonly Color WaveformBackground = Color.white;
        private static GUIStyle _waveformStyle = new GUIStyle();
        private static string _currentClipKey;
        private static bool _shouldRepaint;

        private static readonly Dictionary<PlaybackState, Action<SerializedProperty, AudioClip>> _stateActions =
            new Dictionary<PlaybackState, Action<SerializedProperty, AudioClip>>
        {
            { PlaybackState.Play, PlayClip },
            { PlaybackState.Stop, StopClip }
        };

        static AudioClipDrawer()
        {
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (_shouldRepaint)
            {
                var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();
                foreach (var window in windows)
                {
                    if (window.GetType().Name == "InspectorWindow")
                        window.Repaint();
                }
                _shouldRepaint = false;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.objectReferenceValue is AudioClip clip)
            {
                float totalWidth = position.width;
                Rect fieldRect = new Rect(position.x, position.y, totalWidth * 0.72f, position.height);
                EditorGUI.PropertyField(fieldRect, property, label);

                Rect buttonArea = new Rect(fieldRect.xMax + 4, position.y, totalWidth - fieldRect.width - 4, position.height + 2);
                DrawPlayButton(buttonArea, property, clip);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }

            EditorGUI.EndProperty();
        }

        private void DrawPlayButton(Rect rect, SerializedProperty property, AudioClip clip)
        {
            string key = GetClipKey(property);
            bool isPlaying = key == _currentClipKey && AudioUtilInternal.IsClipPlaying();

            PlaybackState state = isPlaying ? PlaybackState.Stop : PlaybackState.Play;
            string btnLabel = isPlaying ? "■" : "►";

            Rect buttonRect = new Rect(rect.x, rect.y, 30, rect.height);
            Rect waveformRect = new Rect(rect.x + 34, rect.y, rect.width - 34, rect.height);

            // Draw waveform background
            if (Event.current.type == EventType.Repaint)
            {
                Texture2D preview = AssetPreview.GetAssetPreview(clip);
                if (preview != null)
                {
                    GUI.color = WaveformBackground;
                    _waveformStyle.normal.background = preview;
                    _waveformStyle.Draw(waveformRect, GUIContent.none, false, false, false, false);
                    GUI.color = Color.white;
                }
            }

            // Draw progress bar when playing
            if (isPlaying)
            {
                int sampleCount = AudioUtilInternal.GetSampleCount(clip);
                int samplePosition = AudioUtilInternal.GetSamplePosition();
                float pct = sampleCount > 0 ? Mathf.Clamp01((float)samplePosition / sampleCount) : 0f;
                Rect progRect = waveformRect;
                progRect.width *= pct;
                GUI.Box(progRect, GUIContent.none, "SelectionRect");
                _shouldRepaint = true;
            }

            if (GUI.Button(buttonRect, btnLabel))
            {
                AudioUtilInternal.StopAll();
                _stateActions[state](property, clip);
                EditorUtility.SetDirty(property.serializedObject.targetObject);
                EditorGUIUtility.ExitGUI();
            }
        }

        private static void PlayClip(SerializedProperty property, AudioClip clip)
        {
            _currentClipKey = GetClipKey(property);
            AudioUtilInternal.Play(clip);
        }

        private static void StopClip(SerializedProperty property, AudioClip clip)
        {
            _currentClipKey = null;
            AudioUtilInternal.StopAll();
        }

        private static string GetClipKey(SerializedProperty property)
            => property.serializedObject.targetObject.GetInstanceID() + ":" + property.propertyPath;
    }

    internal static class AudioUtilInternal
    {
        private static readonly Type _audioUtilType;
        private static readonly Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();

        static AudioUtilInternal()
        {
            Assembly asm = typeof(AudioImporter).Assembly;
            _audioUtilType = asm.GetType("UnityEditor.AudioUtil");
        }

        private static MethodInfo GetMethod(string name, params Type[] parameters)
        {
            if (_audioUtilType == null) return null;
            string key = name + parameters.Length;
            if (!_methods.TryGetValue(key, out var mi))
            {
                mi = _audioUtilType.GetMethod(name, BindingFlags.Static | BindingFlags.Public, null, parameters, null);
                _methods[key] = mi;
            }
            return mi;
        }

        public static void Play(AudioClip clip) => Invoke("PlayPreviewClip", clip, 0, false);
        public static void StopAll() => Invoke("StopAllPreviewClips");
        public static bool IsClipPlaying() => (bool)InvokeWithReturn("IsPreviewClipPlaying");
        public static int GetSamplePosition() => (int)InvokeWithReturn("GetPreviewClipSamplePosition");
        public static int GetSampleCount(AudioClip clip) => (int)InvokeWithReturn("GetSampleCount", clip);

        private static object InvokeWithReturn(string name, params object[] args)
        {
            try
            {
                Type[] paramTypes = Array.ConvertAll(args, a => a?.GetType() ?? typeof(object));
                MethodInfo mi = GetMethod(name, paramTypes);
                return mi?.Invoke(null, args);
            }
            catch { return null; }
        }

        private static void Invoke(string name, params object[] args)
        {
            try
            {
                Type[] paramTypes = Array.ConvertAll(args, a => a?.GetType() ?? typeof(object));
                MethodInfo mi = GetMethod(name, paramTypes);
                mi?.Invoke(null, args);
            }
            catch { }
        }
    }
}
#endif