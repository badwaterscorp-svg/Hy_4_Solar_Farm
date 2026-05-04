using UnityEngine;

public abstract class BaseMissionHandler : MonoBehaviour, IMission
{
    public abstract void StartMission();
    public abstract void CompleteMission();
}