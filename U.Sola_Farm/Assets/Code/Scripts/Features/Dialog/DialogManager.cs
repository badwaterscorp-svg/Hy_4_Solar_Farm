using System;
using UnityEngine;

public class DialogManager : MonoBehaviour 
{
    [SerializeField] DialogHandler dialogHandler;
    [SerializeField] private DialogSheet dialogSheet;
    [SerializeField] private GameObject containerDialog;

    private void OnEnable()
    {
        dialogHandler.OnDialogFinished += EnDialog;
    }


    private void OnDisable()
    {
        dialogHandler.OnDialogFinished -= EnDialog;
    }

    private void Update()
    {
        print("Running:"+dialogHandler.IsRunning);
    }

    private void EnDialog()
    {
        containerDialog.SetActive(false);
    }
    public void Do()
    {
        if(dialogHandler.IsRunning)
            Stop();
        else if(dialogHandler.IsActive)
            dialogHandler.Next();
        else
            dialogHandler.StartDialog(dialogSheet);
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        dialogHandler.ShowFullText();
    }
}