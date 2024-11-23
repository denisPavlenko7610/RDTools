#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEngine;

namespace UnityEditor
{
    public static class AudioUtility
    {
        public static void PlayClip(AudioClip clip, int startSample, bool loop)
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "PlayPreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new[]
                {
                    typeof(AudioClip),
                    typeof(Int32),
                    typeof(Boolean)
                },
                null
            );
            method?.Invoke(
                null,
                new object[]
                {
                    clip,
                    startSample,
                    loop
                }
            );

            SetClipSamplePosition(clip, startSample);
        }

        public static void PlayClip(AudioClip clip)
        {
            PlayClip(clip, 0, false);
        }

        public static bool IsClipPlaying(AudioClip clip)
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "IsPreviewClipPlaying",
                BindingFlags.Static | BindingFlags.Public
            );

            bool playing = (bool)method.Invoke(
                new object[]
                {
                    clip,
                },
                null
            );

            return playing;
        }

        public static void StopAllClips()
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "StopAllPreviewClips",
                BindingFlags.Static | BindingFlags.Public
            );

            method?.Invoke(
                null,
                null
            );
        }

        public static int GetClipSamplePosition(AudioClip clip)
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "GetPreviewClipSamplePosition",
                BindingFlags.Static | BindingFlags.Public
            );

            int position = (int)method.Invoke(
                new object[]
                {
                    clip
                },
                null
            );

            return position;
        }

        private static void SetClipSamplePosition(AudioClip clip, int iSamplePosition)
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "SetPreviewClipSamplePosition",
                BindingFlags.Static | BindingFlags.Public
            );
            method?.Invoke(
                new object[]
                {
                    clip,
                    iSamplePosition
                },
                new object[]
                    { clip, iSamplePosition }
            );
        }

        public static int GetSampleCount(AudioClip clip)
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "GetSampleCount",
                BindingFlags.Static | BindingFlags.Public
            );

            int samples = (int)method.Invoke(
                new object[]
                {
                    clip
                },
                new object[]
                {
                    clip
                }
            );

            return samples;
        }
    }
}
#endif