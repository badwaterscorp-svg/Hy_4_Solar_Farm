using System;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Build;

[InitializeOnLoad]
public static class DirectivaDoTweens
{
    static DirectivaDoTweens()
    {
        string scriptDirectiva = "Assets/Plugins/Demigiant/DOTweenPro/DOTweenPro.dll";
        string symboDefinition = "ANIMA_DOTWEEN_PRO";

        if (File.Exists(scriptDirectiva))
        {
            AddDefineSymbol(symboDefinition);
            //Debug.Log("Existe");
        }
        else
        {
            //Debug.Log("No ERxiste");
            RemoveDefine(symboDefinition);
        }
    }

    private static void AddDefineSymbol(string definesSymbol)
    {
        var targetGroup = NamedBuildTarget.Standalone;
        string defines = PlayerSettings.GetScriptingDefineSymbols(targetGroup);
        if (!defines.Contains(definesSymbol))
        {
            PlayerSettings.SetScriptingDefineSymbols(targetGroup, defines + ";" + definesSymbol);
            Debug.Log("Enter");
        }
    }

    private static void RemoveDefine(string defineSymbol)
    {
        var targetGroup = NamedBuildTarget.Standalone;
        string defines = PlayerSettings.GetScriptingDefineSymbols(targetGroup);
        if (defines.Contains(defineSymbol))
        {
            PlayerSettings.SetScriptingDefineSymbols(targetGroup, defines.Replace(defineSymbol, "").Replace(";;", ";"));
        }
    }
}
#endif