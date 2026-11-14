using UnityEngine;
using Zenject;

public class MissionSeeCinematic : BaseMissionHandler
{
    [SerializeField] private string _cinematicId;

    [Inject] private ICinematicDataService _dataService;

    private bool _wasSeen;

    private void Awake()
    {
        print($"[MissionSeeCinematic] Awake: Cinematic ID: {GetMissionState()}");
    }

    public override void StartMission()
    {
        _wasSeen = _dataService.HasBeenSeen(_cinematicId);
        Debug.Log($"[MissionSeeCinematic] Cinematic '{_cinematicId}' already seen: {_wasSeen}");
    }

    public override void CompleteMission()
    {
        _dataService.SaveSeen(_cinematicId);
        Debug.Log($"[MissionSeeCinematic] Cinematic '{_cinematicId}' marked as seen");
    }

    public override string GetMissionState()
    {
        return _dataService.HasBeenSeen(_cinematicId) ?"Pass":"NoPass";
    }
}