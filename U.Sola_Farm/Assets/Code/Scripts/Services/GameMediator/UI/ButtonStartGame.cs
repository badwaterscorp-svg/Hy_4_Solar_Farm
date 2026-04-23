using B_Extensions;
using System.Collections;
using TMPro;
using UnityEngine;

public class ButtonStartGame:BaseButtonAttendant
{
    private void Start() => buttonComponent.onClick.AddListener(StartGame);

    private void StartGame() => GameStateContext.ChangeState(GameEventType.Intro);
}