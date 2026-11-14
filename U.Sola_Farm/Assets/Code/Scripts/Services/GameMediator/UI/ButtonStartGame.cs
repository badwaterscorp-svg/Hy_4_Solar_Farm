using B_Extensions;
using System.Collections;
using TMPro;
using UnityEngine;

public class ButtonStartGame:BaseButtonAttendant
{
    [SerializeField] PlayerHandler playerHandler;
    [SerializeField] GameObject canvasHistory;

    private void Start()
    {
        buttonComponent.onClick.AddListener(StartGame);

        if (PlayerPrefs.GetInt(KeyStorage.IntroWatched_I, 0) == 1)
        { 
            Click();
            playerHandler.StartGame();
            playerHandler.SetFreezeMovement(true);
            canvasHistory.SetActive(false);
        }
    }

    private void StartGame()
    {
        GameStateContext.ChangeState(GameEventType.Intro);
        PlayerPrefs.SetInt(KeyStorage.IntroWatched_I, 1);
        canvasHistory.SetActive(false);
    }
}