using UnityEngine.Events;

public interface IMission
{
    void StartMission();
    void CompleteMission();
    string GetMissionState();
}