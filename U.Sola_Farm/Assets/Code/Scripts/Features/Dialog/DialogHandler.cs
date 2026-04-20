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

    private DialogAnimation _animationHandler;
    private DialogSheet _currentDialog;
    private ITypingAnimaStrategy _currentStrategy;
    private int _currentLineIndex;
    public bool IsActive { get; private set; }

    public event Action OnDialogStarted;
    public event Action OnDialogFinished;
    public event Action<DialogLine> OnLineChanged;

    private void Awake()
    {
        _animationHandler = new DialogAnimation(_dialogText, timePerChar, curve, this, GetStrategy(defaultStrategy));
    }

    public bool IsRunning => _animationHandler.IsRunning;

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
        print(IsActive+"Activo cuando sea false retorna");
        if (!IsActive || _currentStrategy == null) 
            return;
        _animationHandler.ShowFullText();
    }

    private void ShowLine()
    {
        DialogLine line = _currentDialog.lines[_currentLineIndex];
        OnLineChanged?.Invoke(line);

        if (_speakerNameText != null)
            _speakerNameText.text = line.speakerName;
        _animationHandler.AnimateText(line.text);
    }

    private ITypingAnimaStrategy GetStrategy(string strategyName)
    {
        if (string.IsNullOrEmpty(strategyName))
            strategyName = defaultStrategy;

        Type tipo = Type.GetType(strategyName);
        if (tipo != null)
        {
            object instancia = Activator.CreateInstance(tipo);
            if (instancia is ITypingAnimaStrategy asInterface)
                return asInterface;
        }

        Debug.LogWarning($"[DialogHandler] Strategy '{strategyName}' not found, using default.");
        return new AnimCharScaleFade();
    }

    private void EndDialog()
    {
        IsActive = false;
        OnDialogFinished?.Invoke();
    }
}
