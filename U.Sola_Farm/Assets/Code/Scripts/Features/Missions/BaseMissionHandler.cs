using UnityEngine;
using UnityEngine.Events;

public abstract class BaseMissionHandler : MonoBehaviour, IMission
{
    [field: SerializeField] UnityEvent OnMissionComplete;

    public abstract void StartMission();
    public abstract void CompleteMission();
    public abstract string GetMissionState();
}