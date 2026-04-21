using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

public class DialogHandler : MonoBehaviour
{
    [Header("--UI--")]
    [SerializeField] private TMP_Text _speakerNameText;
    [SerializeField] private TMP_Text _dialogText;
    [Header("--Animation--")]
    [SerializeField] private float timePerChar = 0.2f;
    [SerializeField] private Ease curve = Ease.OutBack;
    [SerializeField] private string defaultStrategy = "AnimCharScaleFade";

    private DialogSheet _currentDialog;
    private int _currentLineIndex;
    public bool IsActive { get; private set; }
    public bool IsRunning { get; private set; }

    public event Action OnDialogStarted;
    public event Action OnDialogFinished;
    public event Action<DialogLine> OnLineChanged;

    public void StartDialog(DialogSheet dialog)
    {
        _currentDialog = dialog;
        _currentLineIndex = 0;
        IsActive = true;
        OnDialogStarted?.Invoke();
        ShowLine();
    }

    public void Next()
    {
        if (!IsActive) 
            return;

        _currentLineIndex++;
        if (_currentLineIndex >= _currentDialog.lines.Count)
        {
            EndDialog();
            return;
        }
        ShowLine();
    }

    public void ShowFullText()
    {
        if (!IsActive) 
            return;
        DialogLine line = _currentDialog.lines[_currentLineIndex];
        _dialogText.DOKill();
        _dialogText.text = line.text;
        IsRunning = false;
    }

    private void ShowLine()
    {
        DialogLine line = _currentDialog.lines[_currentLineIndex];
        OnLineChanged?.Invoke(line);
        _speakerNameText.text = line.speakerName;
        _dialogText.text = "";
        if (_dialogText != null)
            _dialogText.DOText(line.text, line.text.Length * timePerChar).OnStart(()=>IsRunning = true).OnComplete(()=> IsRunning = false);
    }


    private void EndDialog()
    {
        IsActive = false;
        OnDialogFinished?.Invoke();
    }
}
