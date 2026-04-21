using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour 
{
    [SerializeField] DialogHandler dialogHandler;
    [SerializeField] private DialogSheet dialogSheet;
    [SerializeField] private GameObject containerDialog;
    [SerializeField] TMP_Text speakerNameText;

    private void OnEnable()
    {
        dialogHandler.OnDialogFinished += EnDialog;
    }

    private void OnDisable()
    {
        dialogHandler.OnDialogFinished -= EnDialog;
    }

    private void EnDialog()
    {
        containerDialog.SetActive(false);
    }

    public void Do()
    {
        if (dialogHandler.IsRunning)
        {
            Stop();
            print("Entra a stop");
        }
        else if (dialogHandler.IsActive)
        {
            print("Entra a Next");
            dialogHandler.Next();
        }
        else
        {
            print("Entra a Dialog");
            dialogHandler.StartDialog(dialogSheet);
        }
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        dialogHandler.ShowFullText();
    }
}