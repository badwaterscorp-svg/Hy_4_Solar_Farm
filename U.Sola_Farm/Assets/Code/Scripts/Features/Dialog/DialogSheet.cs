using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogData", menuName = "SO/Dialog Data")]
public class DialogSheet : ScriptableObject
{
    public List<DialogLine> lines = new List<DialogLine>();
}

[System.Serializable]
public class DialogLine
{
    public string speakerName;
    [TextArea] public string text;
    public string animationStrategy;
}
