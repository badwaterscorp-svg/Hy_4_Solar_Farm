using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[InitializeOnLoad]
public static class AnimaDGVersion
{
    static string versionKey = "AnimaDGVersion_Shown";
    static string version = "1.4.1";
    static AnimaDGVersion()
    {
        if (!SessionState.GetBool(versionKey, false))
        {
            Debug.Log($"Anima DG Version {version}");
            SessionState.SetBool(versionKey, true);
        }
    }
}
#endif
