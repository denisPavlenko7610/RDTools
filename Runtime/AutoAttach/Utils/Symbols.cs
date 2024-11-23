using UnityEditor;
using UnityEditor.Build;

namespace RDTools.AutoAttach.Utils
{
    public static class Symbols
    {
        public static void AddSymbol(string define)
        {
#if UNITY_EDITOR
            NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            string defines = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);
            if (defines.Contains(define))
                return;
            
            PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, $"{defines};{define}");
#endif
        }
    }
}