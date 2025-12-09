using UnityEditor;
using UnityEditor.Build;

namespace StarterAssets
{
    public static class ScriptingDefineUtils
    {
        private static NamedBuildTarget GetNamedBuildTarget()
        {
            return NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        }

        public static bool CheckScriptingDefine(string scriptingDefine)
        {
            var buildTarget = GetNamedBuildTarget();
            string defines = PlayerSettings.GetScriptingDefineSymbols(buildTarget);
            return defines.Contains(scriptingDefine);
        }

        public static void SetScriptingDefine(string scriptingDefine)
        {
            var buildTarget = GetNamedBuildTarget();
            string defines = PlayerSettings.GetScriptingDefineSymbols(buildTarget);

            if (!defines.Contains(scriptingDefine))
            {
                if (!string.IsNullOrEmpty(defines))
                    defines += ";" + scriptingDefine;
                else
                    defines = scriptingDefine;

                PlayerSettings.SetScriptingDefineSymbols(buildTarget, defines);
            }
        }

        public static void RemoveScriptingDefine(string scriptingDefine)
        {
            var buildTarget = GetNamedBuildTarget();
            string defines = PlayerSettings.GetScriptingDefineSymbols(buildTarget);

            if (defines.Contains(scriptingDefine))
            {
                string newDefines = defines.Replace(scriptingDefine, "");

                // Clean multiple semicolons
                newDefines = newDefines.Replace(";;", ";");

                PlayerSettings.SetScriptingDefineSymbols(buildTarget, newDefines);
            }
        }
    }
}
