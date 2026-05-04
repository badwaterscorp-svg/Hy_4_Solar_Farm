using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField]
    private List<MissionHolder> _missions = new List<MissionHolder>();

    public void StartMission(int index)
    {
        if (index >= 0 && index < _missions.Count && _missions[index].Mission != null)
            _missions[index].Mission.StartMission();
    }

    public void CompleteMission(int index)
    {
        if (index >= 0 && index < _missions.Count && _missions[index].Mission != null)
            _missions[index].Mission.CompleteMission();
    }

    public void StartAllMissions()
    {
        foreach (var holder in _missions)
        {
            if (holder.Mission != null)
                holder.Mission.StartMission();
        }
    }
}

[System.Serializable]
public class MissionHolder
{
    [RequireBadInterface(typeof(IMission))]
    [SerializeField] private MonoBehaviour _mission;
    public IMission Mission => _mission as IMission;
}